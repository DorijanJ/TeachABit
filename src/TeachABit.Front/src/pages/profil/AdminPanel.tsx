import { useEffect, useState } from "react";
import requests from "../../api/agent";
import {
    Box,
    Card,
    CardContent,
    IconButton,
    InputLabel,
    MenuItem,
    Select,
    Typography,
} from "@mui/material";
import SaveIcon from "@mui/icons-material/Save";

export default function AdminPanel() {
    const [roles, setRoles] = useState<string[]>([]);

    const GetAllRoles = async () => {
        const response = await requests.getWithLoading("roles");
        if (response && response.data) {
            setRoles(response.data);
        }
    };

    useEffect(() => {
        GetAllRoles();
    }, []);

    const [selectedRole, setSelectedRole] = useState<string>();

    return (
        <Box sx={{ padding: 2, display: "flex", justifyContent: "center" }}>
            <Card
                sx={{
                    width: "500px",
                    height: "130px",
                    boxShadow: 3,
                }}
            >
                <CardContent>
                    <Typography variant="h6" gutterBottom>
                        Upravljanje ulogama korisnika
                    </Typography>
                    <Box
                        display="flex"
                        flexDirection="row"
                        gap="10px"
                        alignItems="center"
                        width="100%"
                        marginBottom={2}
                    >
                        <InputLabel id="uloga-select-label">Uloga</InputLabel>
                        <div style={{ width: "calc(100% - 120px)" }}>
                            <Select
                                id="uloga-select"
                                value={selectedRole}
                                fullWidth
                                onChange={(e) =>
                                    setSelectedRole(e.target.value)
                                }
                            >
                                {roles.map((role, index) => (
                                    <MenuItem key={index} value={role}>
                                        {role}
                                    </MenuItem>
                                ))}
                            </Select>
                        </div>
                        <IconButton
                            sx={{
                                width: "50px",
                                height: "50px",
                                border: "1px solid #922728",
                            }}
                        >
                            <SaveIcon color="primary" />
                        </IconButton>
                    </Box>
                </CardContent>
            </Card>
        </Box>
    );
}
