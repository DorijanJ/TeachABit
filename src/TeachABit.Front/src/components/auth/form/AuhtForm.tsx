import { Tab, Tabs, Dialog, Button } from "@mui/material";
import { useState } from "react";
import localStyles from "./AuthForm.module.css";
import GoogleAuth from "./pages/google/GoogleAuth";
import LoginForm from "./pages/login/LoginForm";
import RegisterForm from "./pages/register/RegisterForm";
import LoginIcon from "@mui/icons-material/Login";

const authFormTabs = ["Login", "Register"];

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
                sx={{ width: "80%", margin: "20px" }}
                onClick={() => setSelectedTab(0)}
            >
                Prijava
            </Button>
            {selectedTab !== undefined && (
                <Dialog open onClose={onClose}>
                    <div className={localStyles.authFormContainer}>
                        <Tabs variant="fullWidth" value={selectedTab}>
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
                        <GoogleAuth onClose={onClose} />
                    </div>
                </Dialog>
            )}
        </>
    );
}
