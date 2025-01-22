import { useParams } from "react-router-dom";
import { useGlobalContext } from "../../context/Global.context";
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

const getHighestLevelUloga = (uloge: Uloga[]) => {
    const role = uloge.reduce((max, obj) =>
        obj.levelPristupa > max.levelPristupa ? obj : max
    );
    return role?.name ?? "";
};

export default function Profil() {
    const { username } = useParams();
    const globalContext = useGlobalContext();

    const [user, setUser] = useState<AppUserDto>();

    const isCurrentUser = useMemo(() => {
        return globalContext.currentUser?.username === username;
    }, [globalContext.currentUser?.username, username]);

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

    const SendVerificaitonRequest = async () => {
        const response = await requests.postWithLoading(
            `account/${username}/verifikacija-zahtjev`
        );
        if (response && response.data && username) GetUserByUsername(username);
    };

    useEffect(() => {
        if (globalContext.hasPermissions(LevelPristupa.Admin)) GetAllRoles();
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
                    justifyContent={"flex-start"}
                    gap="10px"
                >
                    <Card sx={{ width: 400, minWidth: 300 }}>
                        <CardContent
                            sx={{
                                display: "flex",
                                flexDirection: "column",
                                alignItems: "center",
                                gap: "10px",
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
                            {globalContext.userIsLoggedIn === true &&
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
                                <Typography variant="h5">
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
                            </div>
                            <div
                                style={{
                                    display: "flex",
                                    flexDirection: "column",
                                    alignItems: "center",
                                    gap: "10px",
                                }}
                            >
                                {globalContext.hasPermissions(
                                    LevelPristupa.Admin
                                ) &&
                                selectedUloga !== "Admin" &&
                                username !==
                                    globalContext.currentUser?.username ? (
                                    <Select
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
                                {username ===
                                    globalContext.currentUser?.username && (
                                    <>
                                        {user.verifikacijaStatusNaziv && (
                                            <p style={{ margin: 0 }}>
                                                {user.verifikacijaStatusNaziv}
                                            </p>
                                        )}
                                        {username ===
                                            globalContext.currentUser
                                                ?.username &&
                                            !user.verifikacijaStatusId && (
                                                <Button
                                                    variant="contained"
                                                    onClick={() =>
                                                        SendVerificaitonRequest()
                                                    }
                                                >
                                                    {
                                                        "Pošalji zahtjev za verifikacijom."
                                                    }
                                                </Button>
                                            )}
                                    </>
                                )}
                            </div>
                        </CardContent>
                    </Card>
                </Box>

                <div>
                    {tecajList.length > 0 && (
                        <>
                            <Typography variant="h6" sx={{ margin: 0 }}>
                                {"Moji tečajevi:"}
                            </Typography>
                            <CustomSliderTecaj
                                tecajevi={tecajList}
                            ></CustomSliderTecaj>
                        </>
                    )}
                    {radionicaList.length > 0 && (
                        <>
                            <Typography variant="h6" sx={{ margin: 0 }}>
                                {"Moje radionice:"}
                            </Typography>
                            <CustomSliderRadionica
                                radionice={radionicaList}
                            ></CustomSliderRadionica>
                        </>
                    )}
                </div>
            </Box>
        )
    );
}
