import { useState } from "react";
import { useGlobalContext } from "../../context/Global.context";
import { Typography, Collapse, Box, Paper, IconButton } from "@mui/material";
import TeachABitRenderer from "../../components/editor/TeachaBitRenderer";
import requests from "../../api/agent";
import AddIcon from "@mui/icons-material/Add";
import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";
import ExpandLessIcon from "@mui/icons-material/ExpandLess";
import ExpandMoreIcon from "@mui/icons-material/ExpandMore";
import { LekcijaDto } from "../../models/LekcijaDto";
import LekcijaPopup from "./LekcijaPopup";
import PotvrdiPopup from "../../components/dialogs/PotvrdiPopup";
import { LevelPristupa } from "../../enums/LevelPristupa";

interface Props {
    refreshData: () => Promise<any>;
    lekcije: LekcijaDto[];
    tecajId: number;
    vlasnikId?: string;
}

export default function Lekcije(props: Props) {
    const globalContext = useGlobalContext();
    let lekcijeCounter = 0;

    const [otvorenalekcija, setExpandedLesson] = useState<number | null>(null);
    const togglelekcija = (lekcijaId: number | undefined) => {
        if (lekcijaId)
            setExpandedLesson(otvorenalekcija === lekcijaId ? null : lekcijaId);
    };

    /* otvaranje i zatvaranje prozora za stvaranje ili uredivanje lekcije */
    // popupOpenId: -1 => otvoren popup za stvaranje nove lekcije
    //              1+ (== lekcijaId) => otvoren popup za uredivanje postojece lekcije
    //              null => zatvoreni popup
    const [popupOpenId, setLekcijaDialogOpen] = useState<number | null>(null);
    const handleLekcijaPopupOpen = (lekcijaId: number | undefined) => {
        if (lekcijaId) setLekcijaDialogOpen(lekcijaId);
        else setLekcijaDialogOpen(-1);
    };
    const handleLekcijaPopupClose = () => setLekcijaDialogOpen(null);

    /* potvrda za brisanje lekcije */
    const [isPotvrdaOpen, setIsPotvrdaOpen] = useState(false);
    const [lekcijaIdToDelete, setLekcijaIdToDelete] = useState<
        number | undefined
    >(undefined);
    const handleOpenPotvrda = (lekcijaId: number | undefined) => {
        setLekcijaIdToDelete(lekcijaId);
        setIsPotvrdaOpen(true);
    };
    const handleClosePotvrda = () => {
        setLekcijaIdToDelete(undefined);
        setIsPotvrdaOpen(false);
    };

    const handleIzbrisiLekcija = async (lekcijaId: number | undefined) => {
        if (lekcijaId) {
            const response = await requests.deleteWithLoading(
                `tecajevi/lekcije/${lekcijaId}`
            );
            if (response) {
                handleClosePotvrda();
                props.refreshData();
            }
        }
    };

    return (
        <Box sx={{}}>
            <LekcijaPopup
                refreshData={props.refreshData}
                onClose={handleLekcijaPopupClose}
                isOpen={popupOpenId == -1}
                tecajId={props.tecajId}
            />
            <LekcijaPopup
                refreshData={props.refreshData}
                onClose={handleLekcijaPopupClose}
                isOpen={popupOpenId && popupOpenId !== -1 ? true : false}
                lekcija={props.lekcije.find(
                    (lekcija) => lekcija.id === popupOpenId
                )}
                tecajId={props.tecajId}
                editing={true}
            />
            {isPotvrdaOpen && (
                <PotvrdiPopup
                    onConfirm={() => handleIzbrisiLekcija(lekcijaIdToDelete)}
                    onClose={handleClosePotvrda}
                    tekstPitanje="Jeste li sigurni da želite izbrisati lekciju?"
                    tekstOdgovor="Izbriši"
                />
            )}
            {props.lekcije.map((lekcija) => (
                <div key={"lekcija" + lekcija.id}>
                    <Paper
                        key={lekcija.id}
                        sx={{
                            mb: 2,
                            padding: 2,
                            backgroundColor: "#f9f9f9",
                        }}
                        elevation={1}
                    >
                        {/* Naslov koji otvara lekciju */}
                        <Box
                            sx={{
                                display: "flex",
                                alignItems: "center",
                                justifyContent: "space-between",
                                cursor: "pointer",
                            }}
                            onClick={() => togglelekcija(lekcija.id)}
                        >
                            {/* Naziv lekcije */}
                            <Typography variant="h6">
                                {++lekcijeCounter}. {lekcija.naziv}
                            </Typography>
                            <Box
                                sx={{
                                    display: "flex",
                                    alignItems: "center",
                                }}
                            >
                                {/* Edit i delete gumbi */}
                                {(globalContext.currentUser?.id ===
                                    props.vlasnikId ||
                                    globalContext.hasPermissions(
                                        LevelPristupa.Moderator
                                    )) && (
                                        <>
                                            <IconButton
                                                onClick={(e) => {
                                                    e.stopPropagation();
                                                    handleLekcijaPopupOpen(
                                                        lekcija.id
                                                    );
                                                }}
                                                sx={{
                                                    width: "40px",
                                                    height: "40px",
                                                }}
                                            >
                                                <EditIcon color="primary"></EditIcon>
                                            </IconButton>
                                            <IconButton
                                                onClick={(e) => {
                                                    e.stopPropagation();
                                                    handleOpenPotvrda(lekcija.id);
                                                }}
                                                sx={{
                                                    width: "40px",
                                                    height: "40px",
                                                }}
                                            >
                                                <DeleteIcon color="primary"></DeleteIcon>
                                            </IconButton>
                                        </>
                                    )}

                                {/* strelica gumb/ikona */}
                                <IconButton>
                                    {otvorenalekcija === lekcija.id ? (
                                        <ExpandLessIcon />
                                    ) : (
                                        <ExpandMoreIcon />
                                    )}
                                </IconButton>
                            </Box>
                        </Box>

                        {/* Sadrzaj lekcije */}
                        <Collapse
                            in={otvorenalekcija === lekcija.id}
                            timeout="auto"
                            unmountOnExit
                        >
                            <TeachABitRenderer content={lekcija.sadrzaj} />
                        </Collapse>
                    </Paper>
                </div>
            ))}

            {/* Gumb za dodavanje lekcija */}
            {(globalContext.currentUser?.id === props.vlasnikId) && (
                <Box
                    sx={{
                        display: "flex",
                        alignItems: "center",
                        justifyContent: "flex-end",
                    }}
                >
                    <IconButton
                        onClick={() => {
                            handleLekcijaPopupOpen(undefined);
                        }}
                        sx={{
                            width: "40px",
                            height: "40px",
                        }}
                    >
                        <AddIcon color="primary"></AddIcon>
                    </IconButton>
                </Box>
            )}
        </Box>
    );
}
