import { useState } from "react";
import {
  Typography,
  Collapse,
  Box,
  Paper,
  IconButton,
} from "@mui/material";
import TeachABitRenderer from "../../components/editor/TeachaBitRenderer";
import ExpandLessIcon from '@mui/icons-material/ExpandLess';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';

interface Props {
  lekcije: {
    id: number;
    naziv: string;
    sadrzaj: string;
    createdDateTime?: string;
    tecajId: number;
  }[];
}

export default function Lekcije(props: Props) {

  let lekcijeCounter = 0

  const [otvorenalekcija, setExpandedLesson] = useState<number | null>(1);

  const togglelekcija = (lekcijaId: number) => {
    setExpandedLesson(otvorenalekcija === lekcijaId ? null : lekcijaId);
  };

  return (

    <Box sx={{ p: 2 }}>
      {props.lekcije.map((lekcija) => (
        <Paper
          key={lekcija.id}
          sx={{
            mb: 2,
            padding: 2,
            backgroundColor: "#f9f9f9",
          }}
          elevation={3}
        >
          {/* Naslov koji otvara lekciju */}
          <Box
            sx={{
              display: "flex",
              alignItems: "center",
              justifyContent: "space-between",
              cursor: "pointer",
            }}
            onClick={() => togglelekcija(lekcija.id)}
          >
            <Typography variant="h6">{++lekcijeCounter}. {lekcija.naziv}</Typography>
            <IconButton>
              {otvorenalekcija === lekcija.id ? <ExpandLessIcon /> : <ExpandMoreIcon />}
            </IconButton>
          </Box>

          {/* Sadrzaj lekcije */}
          <Collapse in={otvorenalekcija === lekcija.id} timeout="auto" unmountOnExit>
            <TeachABitRenderer content={lekcija.sadrzaj}/>
          </Collapse>
        </Paper>
      ))}
    </Box>
  );
}
