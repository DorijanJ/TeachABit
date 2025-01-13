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
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableHead,
    TableRow,
    Typography,
} from "@mui/material";
import { AppUserDto } from "../../models/AppUserDto";
import RemoveIcon from "@mui/icons-material/Remove";
import AddIcon from "@mui/icons-material/Add";

interface Props {
    user: AppUserDto;
}

export default function AdminPanel(props: Props) {
    const [roles, setRoles] = useState<string[]>([]);

    const GetAllRoles = async () => {
        const response = await requests.getWithLoading("roles");
        if (response && response.data) {
            setRoles(response.data);
        }
    };

    const userRoles = props.user.roles ?? [];

    useEffect(() => {
        GetAllRoles();
    }, []);

    const [selectedRole, setSelectedRole] = useState<string>();

    return (
        <Box sx={{ padding: 2, display: "flex", justifyContent: "center" }}>
            <Card sx={{ width: "400px", boxShadow: 3 }}>
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
                        <InputLabel id="uloga-select-label">Role</InputLabel>
                        <div style={{ width: "calc(100% - 100px)" }}>
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
                                width: "30px",
                                height: "30px",
                                border: "1px solid #922728",
                            }}
                        >
                            <AddIcon color="primary" />
                        </IconButton>
                    </Box>
                    <TableContainer>
                        <Table>
                            <TableHead>
                                <TableRow>
                                    <TableCell
                                        sx={{
                                            backgroundColor: "#f5f5f5",
                                            fontWeight: "bold",
                                            borderBottom: "2px solid #ddd",
                                        }}
                                    >
                                        Uloga
                                    </TableCell>
                                    <TableCell
                                        sx={{
                                            backgroundColor: "#f5f5f5",
                                            fontWeight: "bold",
                                            borderBottom: "2px solid #ddd",
                                        }}
                                    />
                                </TableRow>
                            </TableHead>
                            <TableBody>
                                {userRoles.length > 0 ? (
                                    userRoles.map((role, index) => (
                                        <TableRow key={index}>
                                            <TableCell
                                                sx={{
                                                    padding: "10px",
                                                    borderBottom:
                                                        "1px solid #ddd",
                                                }}
                                            >
                                                {role}
                                            </TableCell>
                                            <TableCell align="right">
                                                <IconButton
                                                    sx={{
                                                        width: "30px",
                                                        height: "30px",
                                                        border: "1px solid #922728",
                                                    }}
                                                >
                                                    <RemoveIcon color="primary" />
                                                </IconButton>
                                            </TableCell>
                                        </TableRow>
                                    ))
                                ) : (
                                    <TableRow>
                                        <TableCell
                                            colSpan={1}
                                            align="center"
                                            sx={{
                                                padding: "10px",
                                                borderBottom: "1px solid #ddd",
                                            }}
                                        >
                                            Nema uloga.
                                        </TableCell>
                                    </TableRow>
                                )}
                            </TableBody>
                        </Table>
                    </TableContainer>
                </CardContent>
            </Card>
        </Box>
    );
}
