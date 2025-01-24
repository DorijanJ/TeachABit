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
import NumberRangeSelector from "../tecajevi/NumberRangeSelector";

interface RadionicaSearch {
  search?: string | undefined;
  minPrice?: number;
  maxPrice?: number;
  minLikes?: number;
  maxLikes?: number;
  ownerUsername?: string;
}

export const Radionice = () => {
  const [radionicaList, setRadionicaList] = useState<RadionicaDto[]>([]);
  const [isOpenRadionicaDialog, setIsOpenRadionicaDialog] = useState(false);

  const { buildRequest } = useRequestBuilder();

  const [searchParams, setSearchParams] = useState<RadionicaSearch>({
    maxLikes: 5,
    minLikes: 1,
    maxPrice: 2000,
    minPrice: 0,
  });

  const GetRadionicaList = async () => {
    console.log("Fetching radionice with params:", searchParams);
    const response = await requests.getWithLoading(
      buildRequest("radionice", {
        search: searchParams?.search,
        vlasnikUsername: searchParams?.ownerUsername,
        minCijena: searchParams?.minPrice,
        maxCijena: searchParams?.maxPrice,
        minOcijena: searchParams?.minLikes,
        maxOcijena: searchParams?.maxLikes,
      })
    );
    if (response && response.data) {
        console.log("Fetched radionice:", response.data);
        setRadionicaList(response.data);
  }
};

  useEffect(() => {
    console.log("Updated searchParams: ", searchParams);
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
        <SearchBox
          onSearch={(s) => {
            setSearchParams((prev: any) => ({
              ...prev,
              search: s,
            }));
            GetRadionicaList();
          }}
        />

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
          display: "flex",
          gap: "50px",
          alignItems: "flex-start",
          flexWrap: "wrap",
        }}
      >
        <div style={{ width: "300px" }}>
          <NumberRangeSelector
            min={1}
            max={5}
            onChangeCommited={(_value: number[]) => GetRadionicaList()}
            onChange={(value: number[]) => {
              setSearchParams((prev: any) => ({
                ...prev,
                minLikes: value[0],
                maxLikes: value[1],
              }));
            }}
          />

          <div>
            Raspon ocjene: {searchParams?.minLikes} - {" "}{searchParams?.maxLikes}
          </div>
        </div>

        <div style={{ width: "300px" }}>
          <NumberRangeSelector
            min={0}
            minDistance={10}
            onChangeCommited={(_value: number[]) => {
              GetRadionicaList();
            }}
            max={2000}
            onChange={(value: number[]) => {
              setSearchParams((prev: any) => ({
                ...prev,
                minPrice: value[0],
                maxPrice: value[1],
              }));
            }}
          />

          <div>
            Raspon cijene: ${searchParams?.minPrice} - ${searchParams?.maxPrice}
          </div>
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
            <Radionica key={"radionica" + radionica.id} radionica={radionica} />
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
    </div>
  );
};

export default observer(Radionice);
