import { Navigate } from "react-router-dom";
import { useGlobalContext } from "../../../context/Global.context";
import GenericRoute from "./GenericRoute";

interface PrivateRouteProps {
    page: JSX.Element;
    withNavigation?: boolean;
}

export default function PrivateRoute(props: PrivateRouteProps) {
    const globalContext = useGlobalContext();

    const isLoggedIn = globalContext.userIsLoggedIn;

    if (isLoggedIn === undefined) return <></>;

    return isLoggedIn === true ? (
        <GenericRoute page={props.page} withNavigation={props.withNavigation} />
    ) : (
        <Navigate to="/login" />
    );
}
