import SearchBox from "../../components/searchbox/SearchBox";

// temp function
function abc(query?: string) {
    console.log("Search:", query);
}

export default function Radionice() {
    return (
        <>
            <SearchBox onSearch={abc} />
        </>
    );
}