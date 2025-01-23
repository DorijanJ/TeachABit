import { Navigate } from "react-router-dom";
import { useGlobalContext } from "../../../context/Global.context";
import GenericRoute from "./GenericRoute";
import { LevelPristupa } from "../../../enums/LevelPristupa";

const USERNAME_KEY = "username";

interface PrivateRouteProps {
    page: JSX.Element;
    withNavigation?: boolean | undefined;
    accessLevel?: LevelPristupa;
}

export default function PrivateRoute(props: PrivateRouteProps) {
    const globalContext = useGlobalContext();

    var isLoggedIn = globalContext.userIsLoggedIn;

    if (isLoggedIn === undefined) {
        isLoggedIn = localStorage.getItem(USERNAME_KEY) !== null;
    }
    if (isLoggedIn == null) return <Navigate to="/" />;
    if (props.accessLevel && !globalContext.hasPermissions(props.accessLevel))
        return <Navigate to="/" />;

    return isLoggedIn === true ? (
        <GenericRoute page={props.page} withNavigation={props.withNavigation} />
    ) : (
        <Navigate to="/" />
    );
}
