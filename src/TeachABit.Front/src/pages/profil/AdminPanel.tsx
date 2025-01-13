import { useEffect, useState } from "react";
import requests from "../../api/agent";
import { Box, Card, CardContent, IconButton, InputLabel, MenuItem, Select, Table, TableBody, TableCell, TableHead, TableRow } from "@mui/material";
import { AppUserDto } from "../../models/AppUserDto";
import AddIcon from "@mui/icons-material/Add"

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
    }

    const userRoles = props.user.roles ?? [];

    useEffect(() => {
        GetAllRoles();
    }, [])

    const [selectedRole, setSelectedRole] = useState<string>();

    return <>
        <Card sx={{ width: "300px" }}>
            <CardContent sx={{ width: "100%" }}>
                <Box display="flex" flexDirection="row" gap="10px" alignItems={"center"} width={"100%"}>
                    <InputLabel id="uloga-select-label">Uloga</InputLabel>
                    <div style={{ width: "calc(100% - 100px)" }}>
                        <Select
                            id="uloga-select"
                            value={selectedRole}
                            fullWidth
                            onChange={(e) => setSelectedRole(e.target.value)}
                        >
                            {roles.map(role => <MenuItem value={role}>{role}</MenuItem>)}
                        </Select>
                    </div>
                    <IconButton
                        sx={{
                            width: "30px",
                            height: "30px",
                            border: "1px solid #922728"
                        }}>
                        <AddIcon color="primary" />
                    </IconButton>
                </Box>
                <Table>
                    <TableHead>
                        <TableRow>
                            <TableCell>{"Uloga"}</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {userRoles.map(role => <TableRow>
                            <TableCell>
                                {role}
                            </TableCell>
                        </TableRow>)}
                    </TableBody>
                </Table>
            </CardContent>
        </Card >
    </>
}