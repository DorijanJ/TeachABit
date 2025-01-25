namespace TeachABit.Service.Util.Mail
{
    public class MailDescriber
    {
        public static string PasswordResetMail(string username, string resetUrl) =>
            $@"
            <html>
                 <body style='font-family: Arial, sans-serif; color: #333; background-color: #f4f4f4; padding: 20px; width: 800px'>
                    <div style='max-width: 600px; margin: auto; background: #fff; padding: 20px; border-radius: 5px; box-shadow: 0 0 10px rgba(0,0,0,0.1);'>
                        <h2 style='color: #5a9;'>Zahtjev za resetiranje lozinke</h2>
                        <p>Pozdrav <strong>{username}</strong>,</p>
                        <p>Primili smo zahtjev za resetiranje lozinke za vaš TeachABit račun.</p>
                        <p>Molimo vas da koristite donju poveznicu za resetiranje lozinke:</p>
                        <p style='text-align: center; margin: 60px'>
                            <a href='{resetUrl}' style='background-color: #5a9; color: white; padding: 10px 15px; text-decoration: none; border-radius: 5px;  min-width: 200px'>Resetirajte svoju lozinku</a>
                        </p>
                        <p>Ova poveznica vrijedi samo ograničeno vrijeme. Ako niste tražili resetiranje lozinke, slobodno ignorirajte ovaj email.</p>
                        <p>S poštovanjem,<br/>support@TeachABit</p>
                    </div>
                </body>
            </html>";

        public static string EmailConfirmationMail(string confirmationUrl) =>
             $@"
            <html>
                <body style='font-family: Arial, sans-serif; color: #333; background-color: #f4f4f4; padding: 20px; width: 800px'>
                    <div style='max-width: 600px; margin: auto; background: #fff; padding: 20px; border-radius: 5px; box-shadow: 0 0 10px rgba(0,0,0,0.1);'>
                        <h2 style='color: #5a9;'>Potvrda mail adrese</h2>
                        <p>Pozdrav,</p>
                        <p>Primili smo zahtjev za registraciju TeachABit računa s vašom mail adresom.</p>
                        <p>Molimo vas da koristite donju poveznicu za potvrdu mail adrese:</p>
                        <p style='text-align: center; margin: 60px 10px;'>
                            <a href='{confirmationUrl}' style='background-color: #5a9; color: white; padding: 10px 15px; text-decoration: none; border-radius: 5px; min-width: 200px'>Potvrdite mail adresu</a>
                        </p>
                        <p>Ova poveznica vrijedi samo ograničeno vrijeme. Ako niste tražili stvaranje računa, slobodno ignorirajte ovaj email.</p>
                        <p>S poštovanjem,<br/>support@TeachABit</p>
                    </div>
                </body>
            </html>";
        public static string RadionicaPrijava(string radionica, string sadrzaj) =>
             $@"
             <html>
                  <body style='font-family: Arial, sans-serif; color: #333; background-color: #f4f4f4; padding: 20px; width: 800px;'>
                    <div style='max-width: 600px; margin: auto; background: #fff; padding: 20px; border-radius: 5px; box-shadow: 0 0 10px rgba(0,0,0,0.1);'>
                      <h2 style='color: #5a9;'>Prijava na radionicu</h2>
                      <p>Pozdrav,</p>
                      <p>Dostavljamo vam upute za prijavu na radionicu:</p>
                      <h4
                        style='
                          background-color: lightgray;
                          padding: 10px;
                          width: fit-content;
                          word-wrap: break-word;
                          overflow: hidden;
                          text-overflow: ellipsis;
                          max-width: 100%;
                          white-space: wrap;
                        '
                      >
                        {radionica}
                      </h4>
                      <p>Molimo vas da koristite donje upute za pridruživanje radionici:</p>
                      <p style='text-align: center; margin: 10px 10px;'>
                        <p
                          style='
                          padding: 10px;
                          width: fit-content;
                          word-wrap: break-word;
                          overflow: hidden;
                          text-overflow: ellipsis;
                          max-width: 100%;
                          white-space: wrap;'> {sadrzaj} </p>
                      </p>
                      <p>S poštovanjem,<br />support@TeachABit</p>
                    </div>
                  </body>
                </html>";
    }
}
