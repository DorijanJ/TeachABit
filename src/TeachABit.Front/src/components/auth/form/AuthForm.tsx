import { Tab, Tabs, Dialog, Button } from "@mui/material";
import { useState } from "react";
import localStyles from "./AuthForm.module.css";
import GoogleAuth from "./pages/google/GoogleAuth";
import LoginForm from "./pages/login/LoginForm";
import RegisterForm from "./pages/register/RegisterForm";
import LoginIcon from "@mui/icons-material/Login";
import Divider from "@mui/material/Divider";

const authFormTabs = ["Prijava", "Registracija"];

enum AuthFormEnum {
    Login = 0,
    Register = 1,
}

const AuthPageDelegator = (props: {
    selectedPage: number;
    onClose: () => void;
}) => {
    switch (props.selectedPage) {
        case AuthFormEnum.Login:
            return <LoginForm onClose={props.onClose} />;
        case AuthFormEnum.Register:
            return <RegisterForm />;
        default:
            return <>Not implemented</>;
    }
};

export default function AuthButton() {
    const [selectedTab, setSelectedTab] = useState<number>();

    const onClose = () => {
        setSelectedTab(undefined);
    };

    return (
        <>
            <Button
                variant="contained"
                startIcon={<LoginIcon />}
                className={localStyles.myButton}
                sx={{
                    margin: 5,
                }}
                onClick={() => setSelectedTab(0)}
            >
                Prijava
            </Button>
            {selectedTab !== undefined && (
                <Dialog
                    open
                    onClose={onClose}
                    sx={{
                        maxHeight: "500vh",
                        padding: 0,
                    }}
                >
                    <div className={localStyles.authFormContainer}>
                        {/* <img src={Logo} alt="Teach A Bit Logo" style={{width: "20%", height: "auto", marginBottom: 0}}/> */}
                        <Tabs
                            variant="fullWidth"
                            value={selectedTab}
                            textColor="secondary" // Use predefined textColor value
                            indicatorColor="secondary" // Use predefined indicatorColor value
                        >
                            {authFormTabs.map((tab, index) => (
                                <Tab
                                    key={tab}
                                    onClick={() => setSelectedTab(index)}
                                    label={tab}

                                />
                            ))}
                        </Tabs>



                        <AuthPageDelegator
                            selectedPage={selectedTab}
                            onClose={onClose}
                        />
                        <Divider
                            orientation="horizontal"
                            flexItem
                            color="black"
                        />
                        <GoogleAuth onClose={onClose} />
                    </div>
                </Dialog>
            )}
        </>
    );
}
