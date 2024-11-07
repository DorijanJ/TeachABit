import SearchBox from "../../components/searchbox/SearchBox";

// temp function
function abc(query: string) {
    console.log("Search:", query)
}

export default function Tecajevi() {
    return <>
        <SearchBox height={65} width={500} onSearch={abc} />
    </>;
}
