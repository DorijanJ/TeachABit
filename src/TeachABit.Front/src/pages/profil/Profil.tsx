import { useParams } from "react-router-dom";
import {
    Card,
    CardContent,
    Typography,
    Avatar,
    Box,
    Select,
    MenuItem,
    IconButton,
    Button,
} from "@mui/material";
import { useEffect, useMemo, useState } from "react";
import requests from "../../api/agent";
import { AppUserDto } from "../../models/AppUserDto";
import VerifiedIcon from "@mui/icons-material/Verified";
import EditProfilDialog from "./EditProfilDialog";
import Uloga from "../../models/Uloga";
import EditIcon from "@mui/icons-material/Edit";
import { VerifikacijaEnum } from "../../enums/VerifikacijaEnum";
import CustomSliderTecaj from "../profil/CustomSliderTecaj";
import CustomSliderRadionica from "../profil/CustomSliderRadionica";
import { TecajDto } from "../../models/TecajDto";
import { LevelPristupa } from "../../enums/LevelPristupa";
import { RadionicaDto } from "../../models/RadionicaDto";
import { KorisnikStatus } from "../../enums/KorisnikStatus";
import MicOffIcon from "@mui/icons-material/MicOff";
import { observer } from "mobx-react";
import globalStore from "../../stores/GlobalStore";
import { useNavigate } from "react-router-dom";
import DeleteButtonAndPrompt from "./DeleteButtonAndPrompt";
import PotvrdiPopup from "../../components/dialogs/PotvrdiPopup";

const getHighestLevelUloga = (uloge: Uloga[]) => {
    const role = uloge.reduce((max, obj) =>
        obj.levelPristupa > max.levelPristupa ? obj : max
    );
    return role?.name ?? "";
};

export const Profil = () => {
    const navigate = useNavigate();
    const { username } = useParams();

    const [user, setUser] = useState<AppUserDto>();

    const isCurrentUser = useMemo(() => {
        return globalStore.currentUser?.username === username;
    }, [globalStore.currentUser?.username, username]);

    const GetUserByUsername = async (username: string) => {
        const response = await requests.getWithLoading(
            `account/by-username/${username}`
        );
        if (response && response.data) {
            setUser(response.data);
            const uloge: Uloga[] = response.data.roles;
            setSelectedUloga(getHighestLevelUloga(uloge));
        }
    };

    const UtisajKorisnika = async (username: string) => {
        const response = await requests.postWithLoading(
            `account/${username}/utisaj`
        );
        if (response && response.message?.severity === "success") {
            GetUserByUsername(username);
        }
    };

    const OdTisajKorisnika = async (username: string) => {
        const response = await requests.deleteWithLoading(
            `account/${username}/utisaj`
        );
        if (response && response.message?.severity === "success") {
            GetUserByUsername(username);
        }
    };

    const [uloge, setUloge] = useState<Uloga[]>([]);
    const [selectedUloga, setSelectedUloga] = useState<string>();
    const [isOpenImageDialog, setIsOpenImageDialog] = useState(false);

    const GetAllRoles = async () => {
        const response = await requests.getWithLoading("uloge");
        if (response && response.data) {
            setUloge(response.data);
        }
    };

    const UpdateKorisnikUloga = async (uloga: string) => {
        const response = await requests.postWithLoading(
            `account/${username}/postavi-ulogu`,
            {
                roleName: uloga,
            }
        );
        if (response && username) {
            setSelectedUloga(uloga);
            GetUserByUsername(username);
        }
    };

    const [verificationDialog, setVerificationDialog] = useState(false);

    const SendVerificaitonRequest = async () => {
        const response = await requests.postWithLoading(
            `account/${username}/verifikacija-zahtjev`
        );
        if (response && response.data && username) {
            GetUserByUsername(username);
            setVerificationDialog(false);
        }
    };

    const deleteRacun = async () => {
        const isDeletingSelf = globalStore.currentUser?.username === username;
        const response = await requests.deleteWithLoading(
            `account/${username}`
        );
        if (response && response.message?.severity === "success") {
            if (isDeletingSelf) {
                navigate("/tecajevi");
            } else navigate(-1);
        }
    };

    useEffect(() => {
        if (globalStore.hasPermissions(LevelPristupa.Admin)) GetAllRoles();
    }, []);

    useEffect(() => {
        if (username) GetUserByUsername(username);
    }, [username]);

    const [tecajList, setTecajList] = useState<TecajDto[]>([]);
    const [radionicaList, setRadionicaList] = useState<RadionicaDto[]>([]);

    const GetTecajList = async () => {
        const response = await requests.getWithLoading(
            `tecajevi?vlasnikUsername=${username}`
        );
        if (response && response.data) setTecajList(response.data);
    };

    const GetRadionicaList = async () => {
        const response = await requests.getWithLoading(
            `radionice?vlasnikUsername=${username}`
        );
        if (response && response.data) setRadionicaList(response.data);
    };

    useEffect(() => {
        GetTecajList();
        GetRadionicaList();
    }, [username]);

    return (
        user && (
            <Box
                display="flex"
                flexDirection={"column"}
                justifyContent={"flex-start"}
                gap="20px"
            >
                <Box
                    display="flex"
                    flexDirection={"row"}
                    justifyContent={"center"}
                    width={"100%"}
                    gap="10px"
                >
                    <Card sx={{ minWidth: 300, width: "100%" }}>
                        <CardContent
                            sx={{
                                display: "flex",
                                flexDirection: "column",
                                alignItems: "center",
                                gap: "5px",
                            }}
                        >
                            <Avatar sx={{ width: 100, height: 100 }}>
                                {user.id && user.profilnaSlikaVersion ? (
                                    <>
                                        <img
                                            style={{
                                                objectFit: "cover",
                                                width: "100%",
                                                height: "100%",
                                            }}
                                            src={`${
                                                import.meta.env
                                                    .VITE_REACT_AWS_BUCKET
                                            }${user.id}${
                                                user.profilnaSlikaVersion
                                                    ? "?version=" +
                                                      user.profilnaSlikaVersion
                                                    : ""
                                            }`}
                                        />
                                    </>
                                ) : (
                                    <>{user.username ? user.username[0] : ""}</>
                                )}
                            </Avatar>
                            {globalStore.currentUser !== undefined &&
                                isCurrentUser && (
                                    <IconButton
                                        onClick={() =>
                                            setIsOpenImageDialog(true)
                                        }
                                    >
                                        <EditIcon />
                                    </IconButton>
                                )}
                            {isOpenImageDialog && (
                                <EditProfilDialog
                                    onClose={() => {
                                        setIsOpenImageDialog(false);
                                        window.location.reload();
                                    }}
                                />
                            )}
                            <div
                                style={{
                                    display: "flex",
                                    flexDirection: "row",
                                    gap: "10px",
                                    alignItems: "center",
                                }}
                            >
                                <Typography variant="h4" sx={{ margin: 0 }}>
                                    <b>{user.username} </b>
                                </Typography>
                                {user.verifikacijaStatusId ===
                                    VerifikacijaEnum.Verificiran && (
                                    <VerifiedIcon
                                        sx={{
                                            height: "25px",
                                            width: "25px",
                                            color: "#922728",
                                        }}
                                    />
                                )}
                                {user.korisnikStatusId ===
                                    KorisnikStatus.Utisan && (
                                    <MicOffIcon
                                        sx={{
                                            height: "25px",
                                            width: "25px",
                                            color: "#922728",
                                        }}
                                    />
                                )}
                            </div>
                            <div
                                style={{
                                    display: "flex",
                                    flexDirection: "column",
                                    alignItems: "center",
                                    gap: "10px",
                                    height: "110px",
                                }}
                            >
                                {globalStore.hasPermissions(
                                    LevelPristupa.Admin
                                ) &&
                                selectedUloga !== "Admin" &&
                                username !==
                                    globalStore.currentUser?.username ? (
                                    <Select
                                        sx={{ minWidth: "100px" }}
                                        value={selectedUloga}
                                        onChange={(e) =>
                                            UpdateKorisnikUloga(e.target.value)
                                        }
                                    >
                                        {uloge.map((uloga, index) => (
                                            <MenuItem
                                                key={index}
                                                value={uloga.name}
                                            >
                                                {uloga.name}
                                            </MenuItem>
                                        ))}
                                    </Select>
                                ) : (
                                    <>{selectedUloga}</>
                                )}
                                <div
                                    style={{
                                        display: "flex",
                                        flexDirection: "row",
                                        width: "100%",
                                        justifyContent: "center",
                                        gap: "10px",
                                        height: "70px",
                                    }}
                                >
                                    {username ===
                                        globalStore.currentUser?.username && (
                                        <>
                                            {user.verifikacijaStatusId ===
                                                VerifikacijaEnum.ZahtjevPoslan && (
                                                <Button
                                                    disabled
                                                    variant="outlined"
                                                    sx={{ height: "30px" }}
                                                >
                                                    {
                                                        user.verifikacijaStatusNaziv
                                                    }
                                                </Button>
                                            )}
                                            {username ===
                                                globalStore.currentUser
                                                    ?.username &&
                                                !user.verifikacijaStatusId && (
                                                    <Button
                                                        sx={{ height: "30px" }}
                                                        variant="contained"
                                                        onClick={() =>
                                                            setVerificationDialog(
                                                                true
                                                            )
                                                        }
                                                    >
                                                        {
                                                            "Zahtjev za verifikacijom"
                                                        }
                                                    </Button>
                                                )}
                                        </>
                                    )}
                                    {(globalStore.currentUser?.id === user.id ||
                                        (globalStore.hasPermissions(
                                            LevelPristupa.Admin
                                        ) &&
                                            user.roles?.find(
                                                (x) => x.name === "Admin"
                                            ) === undefined)) && (
                                        <DeleteButtonAndPrompt
                                            deleteRacun={deleteRacun}
                                        />
                                    )}
                                    {globalStore.hasPermissions(
                                        LevelPristupa.Moderator
                                    ) &&
                                        user.roles?.find(
                                            (x) =>
                                                x.levelPristupa >=
                                                LevelPristupa.Moderator
                                        ) === undefined && (
                                            <Button
                                                sx={{ height: "30px" }}
                                                onClick={() => {
                                                    if (!username) return;
                                                    if (
                                                        user.korisnikStatusId ===
                                                        KorisnikStatus.Utisan
                                                    )
                                                        OdTisajKorisnika(
                                                            username
                                                        );
                                                    else
                                                        UtisajKorisnika(
                                                            username
                                                        );
                                                }}
                                                variant="contained"
                                            >
                                                {user.korisnikStatusId ===
                                                KorisnikStatus.Utisan
                                                    ? "Odtišaj korisnika"
                                                    : "Utišaj korisnika"}
                                            </Button>
                                        )}
                                </div>
                            </div>
                        </CardContent>
                    </Card>
                </Box>

                <div>
                    {tecajList.length > 0 && (
                        <>
                            <Typography variant="h6" sx={{ margin: 0 }}>
                                {"Tečajevi:"}
                            </Typography>
                            <CustomSliderTecaj
                                tecajevi={tecajList}
                            ></CustomSliderTecaj>
                        </>
                    )}
                    {radionicaList.length > 0 && (
                        <>
                            <Typography variant="h6" sx={{ margin: 0 }}>
                                {"Radionice:"}
                            </Typography>
                            <CustomSliderRadionica
                                radionice={radionicaList}
                            ></CustomSliderRadionica>
                        </>
                    )}
                </div>
                {verificationDialog && (
                    <PotvrdiPopup
                        onClose={() => setVerificationDialog(false)}
                        onConfirm={() => SendVerificaitonRequest()}
                        tekstPitanje="Poslati zahtjev za verifikacijom?"
                        tekstOdgovor="Pošalji"
                    />
                )}
            </Box>
        )
    );
};

export default observer(Profil);
