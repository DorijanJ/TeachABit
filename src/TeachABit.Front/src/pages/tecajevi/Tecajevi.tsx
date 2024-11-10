import SearchBox from "../../components/searchbox/SearchBox";
import localStyles from "./Tecajevi.module.css"

// temp function
function abc(query?: string) {
    console.log("Search:", query);
}

export default function Tecajevi() {
    return (
        <>  <div className={localStyles.center}>
                <SearchBox height={"10vh"} width={"35vw"} onSearch={abc} />
            </div>
        </>
    );
}
