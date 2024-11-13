import { Button } from "@mui/material";
import { useGlobalContext } from "../../context/Global.context";
import SearchBox from "../../components/searchbox/SearchBox";

// temp function
function abc(query?: string) {
  console.log("Search:", query);
}

export default function Radionice() {

    const globalContext = useGlobalContext();

  return (
    <>
      <SearchBox onSearch={abc} />
      
      {globalContext.userIsLoggedIn && (   

                    <Button
                        variant="contained"
                    >
                        Kreiraj novu radionicu
                    </Button>
                    )}
    </>
  );
}