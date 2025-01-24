import { Navigate } from "react-router-dom";
import GenericRoute from "./GenericRoute";
import { observer } from "mobx-react";
import globalStore from "../../../stores/GlobalStore";

interface PrivateRouteProps {
    page: JSX.Element;
    withNavigation?: boolean | undefined;
}

export const PublicRoute = (props: PrivateRouteProps) => {
    if (globalStore.currentUser === undefined) return <Navigate to="/" />;

    return globalStore.currentUser === undefined ? (
        <GenericRoute page={props.page} withNavigation={props.withNavigation} />
    ) : (
        <Navigate to="/dashboard" />
    );
};

export default observer(PublicRoute);
