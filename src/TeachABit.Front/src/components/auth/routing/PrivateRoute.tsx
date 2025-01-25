import { Navigate } from "react-router-dom";
import GenericRoute from "./GenericRoute";
import { LevelPristupa } from "../../../enums/LevelPristupa";
import { observer } from "mobx-react";
import globalStore from "../../../stores/GlobalStore";

interface PrivateRouteProps {
    page: JSX.Element;
    withNavigation?: boolean | undefined;
    accessLevel?: LevelPristupa;
}

export const PrivateRoute = (props: PrivateRouteProps) => {
    if (!globalStore.currentUser) return <Navigate to="/" />;
    if (props.accessLevel && !globalStore.hasPermissions(props.accessLevel))
        return <Navigate to="/" />;

    return globalStore.currentUser !== undefined ? (
        <GenericRoute page={props.page} withNavigation={props.withNavigation} />
    ) : (
        <Navigate to="/" />
    );
};

export default observer(PrivateRoute);
