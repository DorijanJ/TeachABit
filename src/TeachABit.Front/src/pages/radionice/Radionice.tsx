import { Button } from "@mui/material";
import SearchBox from "../../components/searchbox/SearchBox";
import { useEffect, useState } from "react";
import useRequestBuilder from "../../hooks/useRequestBuilder";
import requests from "../../api/agent";
import { RadionicaDto } from "../../models/RadionicaDto";
import Radionica from "./Radionica";
import RadionicaEditor from "./RadionicaEditor";
import { observer } from "mobx-react";
import globalStore from "../../stores/GlobalStore";

export const Radionice = () => {
    const [radionicaList, setRadionicaList] = useState<RadionicaDto[]>([]);
    const [isOpenRadionicaDialog, setIsOpenRadionicaDialog] = useState(false);

    const { buildRequest } = useRequestBuilder();

  const GetRadionicaList = async (search: string | undefined = undefined) => {
    const response = await requests.getWithLoading(
      buildRequest("Radionice", { search })
    );
    if (response && response.data) setRadionicaList(response.data);
  };

  useEffect(() => {
    GetRadionicaList();
  }, []);

    return (
        <div
            style={{
                display: "flex",
                flexDirection: "column",
                gap: "20px",
                alignItems: "flex-start",
                height: "100%",
                width: "100%",
                minWidth: "300px",
            }}
        >
            <div
                style={{
                    display: "flex",
                    flexDirection: "row",
                    gap: "20px",
                    width: "100%",
                    flexWrap: "wrap",
                    alignItems: "center",
                }}
            >
                <SearchBox onSearch={GetRadionicaList} />
                {globalStore.currentUser !== undefined && (
                    <Button
                        variant="contained"
                        onClick={() => setIsOpenRadionicaDialog(true)}
                    >
                        Stvori radionicu
                    </Button>
                )}
            </div>

      <div
        style={{
          color: "#4f4f4f",
          fontSize: 20,
          width: "100%",
        }}
      >
        Nadolazeće radionice:
        <hr style={{ border: "1px solid #cccccc" }} />
      </div>

            <div
                style={{
                    display: "grid",
                    gridTemplateColumns: "repeat(auto-fit, minmax(350px, 1fr))",
                    gap: "20px",
                    maxWidth: "100%",
                    width: "100%",
                    paddingBottom: "20px",
                }}
            >
                {radionicaList.map((radionica) => (
                    <Radionica
                        key={"radionica" + radionica.id}
                        radionica={radionica}
                    />
                ))}
            </div>

            <RadionicaEditor
                refreshData={() => GetRadionicaList()}
                onClose={() => {
                    setIsOpenRadionicaDialog(false);
                }}
                isOpen={isOpenRadionicaDialog}
            />
        </div>
    );
};

export default observer(Radionice);
