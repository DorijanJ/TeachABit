import Box from "@mui/material/Box";
import Navigation from "../../navigation/Navigation";
import SearchBox from "../../searchbox/SearchBox";

interface GenericRouteProps {
    page: JSX.Element;
    withNavigation?: boolean;
    withSearchBox?: boolean;
}

export default function GenericRoute(props: GenericRouteProps) {
    return (
        <>
            {props.withNavigation && <Navigation />}
            <div className="pageView">{props.page}</div>
        </>
    );
}
