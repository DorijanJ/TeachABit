import Navigation from "../../navigation/Navigation";

interface GenericRouteProps {
    page: JSX.Element;
    withNavigation?: boolean | undefined;
}

export default function GenericRoute(props: GenericRouteProps) {
    return (
        <>
            <Navigation isExpanded={props.withNavigation} />
            <div className="pageView">{props.page}</div>
        </>
    );
}
