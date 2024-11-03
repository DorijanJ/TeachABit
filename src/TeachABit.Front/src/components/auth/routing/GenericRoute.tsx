import Navigation from "../../navigation/Navigation";

interface GenericRouteProps {
    page: JSX.Element;
    withNavigation?: boolean;
}

export default function GenericRoute(props: GenericRouteProps) {
    return (
        <>
            {props.withNavigation && <Navigation />}
            {props.page}
        </>
    );
}
