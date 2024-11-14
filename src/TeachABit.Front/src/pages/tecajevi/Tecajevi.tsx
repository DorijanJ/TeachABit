import SearchBox from "../../components/searchbox/SearchBox";

function abc(query?: string) {
    console.log("Search:", query);
}

export default function Tecajevi() {
    return (
        <>
            <div>
                <SearchBox onSearch={abc} />
            </div>
        </>
    );
}
