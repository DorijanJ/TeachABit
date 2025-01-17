import {
    Card,
    CardContent,
    Typography,
    Box,
    IconButton,
    Button,
  } from "@mui/material";
  import UserLink from "../profil/UserLink";
  import { useNavigate } from "react-router-dom";
  import KeyboardArrowRightIcon from "@mui/icons-material/KeyboardArrowRight";
  import { RadionicaDto } from "../../models/RadionicaDto";
  
  interface Props {
    radionica: RadionicaDto;
  }
  
  export default function Radionica(props: Props) {
    const navigate = useNavigate();
  
    return (
      <Card
        id="radionica"
        sx={{
          width: "32%",
          height: "400px",
          borderRadius: "10px",
          boxSizing: "border-box",
          border: "1px solid lightgray",
          minWidth: "300px",
          cursor: "pointer", 
        }}
        onClick={() => {
          navigate(`/radionica/${props.radionica.id}`);
        }}
      >
        <CardContent
          sx={{
            textAlign: "center",
            display: "flex",
            flexDirection: "column",
            justifyContent: "space-between",
            gap: 1,
            height: "100%",
          }}
        >
          <div>
            <Box
              display={"flex"}
              flexDirection={"row"}
              justifyContent={"space-between"}
              alignItems={"flex-start"}
            >
              <Typography
                color="primary"
                variant="h5"
                component="div"
                sx={{
                  overflow: "hidden",
                  textAlign: "left",
                  whiteSpace: "wrap",
                  textOverflow: "ellipsis",
                  maxWidth: "80%",
                }}
              >
                {props.radionica.naziv}
              </Typography>
                {/* 
              <IconButton
                color="primary"
                size="small"
                sx={{ border: "1px solid #3a7ca5" }}
                //onClick={(e) => e.stopPropagation()} // Zaustavlja klik na ikonu
              >
                <KeyboardArrowRightIcon />
              </IconButton>
                */}
            </Box>
            <Typography
              variant="body1"
              component="div"
              sx={{
                overflow: "hidden",
                textAlign: "left",
                whiteSpace: "wrap",
                maxWidth: "100%",
                textOverflow: "ellipsis",
                marginTop: "20px",
              }}
            >
              {props.radionica.opis}
            </Typography>
          </div>
          <Box
            justifyContent={"flex-end"}
            display="flex"
            flexDirection={"row"}
            alignItems="center"
            gap={2}
          >
            {/* Klik na UserLink vodi na profil */}
            <div onClick={(e) => e.stopPropagation()}>
              <UserLink
                user={{
                  id: props.radionica.vlasnikId,
                  username: props.radionica.vlasnikUsername,
                  profilnaSlikaVersion:
                    props.radionica.vlasnikProfilnaSlikaVersion,
                }}
              />
            </div>
            <Box
              display={"flex"}
              alignItems={"flex-end"}
              flexDirection={"row"}
              gap={0.7}
            >
              {props.radionica.cijena && props.radionica.cijena > 0 && (
                <Button disabled variant="contained">
                  {props.radionica.cijena}€
                </Button>
              )}
            </Box>
          </Box>
        </CardContent>
      </Card>
    );
  }
  