import { Tab, Tabs, Dialog } from "@mui/material";
import { observer } from "mobx-react";
import { useState } from "react";
import LoginForm from "./LoginForm";
import RegisterForm from "./RegisterForm";
import localStyles from "./AuthForm.module.css";

const authFormTabs = ["Login", "Register"];

enum AuthFormEnum {
    Login = 0,
    Register = 1,
}

const AuthPageDelegator = (props: { selectedPage: number }) => {
    switch (props.selectedPage) {
        case AuthFormEnum.Login:
            return <LoginForm />;
        case AuthFormEnum.Register:
            return <RegisterForm />;
        default:
            return <>Not implemented</>;
    }
};

interface Props {
    isOpen: boolean;
    onClose: () => void;
}

const AuthForm = observer((props: Props) => {
    const [selectedTab, setSelectedTab] = useState(0);

    return (
        <>
            <Dialog open={props.isOpen} onClose={props.onClose}>
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

                    <AuthPageDelegator selectedPage={selectedTab} />
                </div>
            </Dialog>
        </>
    );
});

export default AuthForm;
