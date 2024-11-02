# TeachABit
Ova platforma omogućuje korisnicima raznovrsne mogućnosti učenja i razmjene znanja. Korisnici mogu objavljivati vlastite tečajeve i radionice, te aktivno sudjelovati u diskusijama foruma. Forumi omogućuju razmjenu ideja, postavljanje pitanja i davanje odgovora. Cilj platforme je unaprijediti pristup obrazovanju i olakšati povezivanje između predavača i zainteresiranih korisnika.

# Opis projekta
Ovaj projekt je reultat timskog rada u sklopu projeknog zadatka kolegija [Programsko inženjerstvo](https://www.fer.unizg.hr/predmet/proinz) na Fakultetu elektrotehnike i računarstva Sveučilišta u Zagrebu. 

Kratko opisati cilj Vašeg projekta. Vaša motivacija?  (Napomena: odgovor nije »Zato što je to bio zadatak i nismo imali ideje za drugo.«). Koji problem rješavate?
> Obzirom da je ovo zadani projekt navedite i što želite/jeste novo  naučili.

> Dobro izrađen opis omogućuje vam da pokažete svoj rad drugim programerima, kao i potencijalnim poslodavcima. Ne samo da prvi dojam na stranici opisa često razlikuje dobar projekt od lošeg projekta već i predstavlja dobru praksu koju morate savladati.

# Funkcionalni zahtjevi
**Korisnik:**
>Pretraživanje stavki (tečaj/radionica/objava)
>* Pretraživanje po naslovu
>* Pretraživanje po broju likeova
>* Pretraživanje po vremenu
>* Pretraživanje po popularnosti
>* Pretraživanje po cijeni (tečaj/radionicu)

> Pregled tuđih profila

> Prijava u sustav
>* Autentifikacija
>* Resetiranje lozinke ("Zaboravio/la sam lozinku.")

> Prijava na stavku (tečaj/radionica)
>* Besplatna prijava (za besplatne stavke)
>* Prijava s plaćanjem (za privatne stavke)

> Kreiranje stavki (tečaj/radionica)
>* Definirati naslov
>* Definiranje opisa
>* Upload slika
>* Definirati lekcije (tečaj)
>* Definiranje kategorije

> Modificiranje stavki (tečaj/radionica/objava)
>* Brisanje stavke
>* Modificiranje stavke
>* Označavanje točnog odgovora (objava)

> Komentiranje stavki (tečaj/radionica/objava)
>* Stvaranje objave kao komentar
>* Lajkanje stavke
>* Favoriziranje stavke

> Personalizacija profila
>* Promjena imena
>* Promjena slike
>* Resetiranje lozinke

> Prijava korisnika za neprimjereno ponašanje

> Prijava za verificiranje profila

**Moderator:**
> Brisanje stavke (tečaj/radionica/objava)

> Verificiranje korisnika

> Utišanje korisnika

**Administrator:**
> Brisanje profila

> Davanje moderatorske uloge

**Server:**
> Obrada zahtjeva
>* Dohvat podataka
>* Izmjena podataka
>* Spremanje podataka


# Tehnologije
* Frontend: [React](https://reactjs.org/), [TypeScript](https://www.typescriptlang.org/)
* Backend: [ASP.NET Core](https://dotnet.microsoft.com/apps/aspnet)
* Baza podataka: [PostgreSQL](https://www.postgresql.org/)
* Dokumentacija: [Github](https://github.com/)
* Plačanje: [Stripe](https://stripe.com/en-hr)
* Mail: [Twilio SendGrid](https://sendgrid.com/en-us)

#Instalcija
# Članovi tima 
| Članovi           | Uloge             |
|:------------------|:------------------|
| Dorijan Jančić    | Devops            |
| Matej Jurišić     | Full stack        |
| Ivan Mitar        | Frontend          |
| Dino Gabrić       | Frontend          |
| Martin Vidmar     | Frontend, Dizajn  |
| Mateo Toić        | Backend           |
| Tomislav Sesar    | Backend           |

# Kontribucije
>Pravila ovise o organizaciji tima i su često izdvojena u CONTRIBUTING.md



# 📝 Kodeks ponašanja [![Contributor Covenant](https://img.shields.io/badge/Contributor%20Covenant-2.1-4baaaa.svg)](CODE_OF_CONDUCT.md)
Kao studenti sigurno ste upoznati s minimumom prihvatljivog ponašanja definiran u [KODEKS PONAŠANJA STUDENATA FAKULTETA ELEKTROTEHNIKE I RAČUNARSTVA SVEUČILIŠTA U ZAGREBU](https://www.fer.hr/_download/repository/Kodeks_ponasanja_studenata_FER-a_procisceni_tekst_2016%5B1%5D.pdf), te dodatnim naputcima za timski rad na predmetu [Programsko inženjerstvo](https://wwww.fer.hr).
Očekujemo da ćete poštovati [etički kodeks IEEE-a](https://www.ieee.org/about/corporate/governance/p7-8.html) koji ima važnu obrazovnu funkciju sa svrhom postavljanja najviših standarda integriteta, odgovornog ponašanja i etičkog ponašanja u profesionalnim aktivnosti. Time profesionalna zajednica programskih inženjera definira opća načela koja definiranju  moralni karakter, donošenje važnih poslovnih odluka i uspostavljanje jasnih moralnih očekivanja za sve pripadnike zajenice.

Kodeks ponašanja skup je provedivih pravila koja služe za jasnu komunikaciju očekivanja i zahtjeva za rad zajednice/tima. Njime se jasno definiraju obaveze, prava, neprihvatljiva ponašanja te  odgovarajuće posljedice (za razliku od etičkog kodeksa). U ovom repozitoriju dan je jedan od široko prihvačenih kodeks ponašanja za rad u zajednici otvorenog koda.
>### Poboljšajte funkcioniranje tima:
>* definirajte načina na koji će rad biti podijeljen među članovima grupe
>* dogovorite kako će grupa međusobno komunicirati.
>* ne gubite vrijeme na dogovore na koji će grupa rješavati sporove primjenite standarde!
>* implicitno podrazmijevamo da će svi članovi grupe slijediti kodeks ponašanja.
 
>###  Prijava problema
>Najgore što se može dogoditi je da netko šuti kad postoje problemi. Postoji nekoliko stvari koje možete učiniti kako biste najbolje riješili sukobe i probleme:
>* Obratite mi se izravno [e-pošta](mailto:vlado.sruk@fer.hr) i  učinit ćemo sve što je u našoj moći da u punom povjerenju saznamo koje korake trebamo poduzeti kako bismo riješili problem.
>* Razgovarajte s vašim asistentom jer ima najbolji uvid u dinamiku tima. Zajedno ćete saznati kako riješiti sukob i kako izbjeći daljnje utjecanje u vašem radu.
>* Ako se osjećate ugodno neposredno razgovarajte o problemu. Manje incidente trebalo bi rješavati izravno. Odvojite vrijeme i privatno razgovarajte s pogođenim članom tima te vjerujte u iskrenost.

# 📝 Licenca
Važeča (1)
[![CC BY-NC-SA 4.0][cc-by-nc-sa-shield]][cc-by-nc-sa]

Ovaj repozitorij sadrži otvoreni obrazovni sadržaji (eng. Open Educational Resources)  i licenciran je prema pravilima Creative Commons licencije koja omogućava da preuzmete djelo, podijelite ga s drugima uz 
uvjet da navođenja autora, ne upotrebljavate ga u komercijalne svrhe te dijelite pod istim uvjetima [Creative Commons Attribution-NonCommercial-ShareAlike 4.0 International License HR][cc-by-nc-sa].
>
> ### Napomena:
>
> Svi paketi distribuiraju se pod vlastitim licencama.
> Svi upotrijebleni materijali  (slike, modeli, animacije, ...) distribuiraju se pod vlastitim licencama.

[![CC BY-NC-SA 4.0][cc-by-nc-sa-image]][cc-by-nc-sa]

[cc-by-nc-sa]: https://creativecommons.org/licenses/by-nc/4.0/deed.hr 
[cc-by-nc-sa-image]: https://licensebuttons.net/l/by-nc-sa/4.0/88x31.png
[cc-by-nc-sa-shield]: https://img.shields.io/badge/License-CC%20BY--NC--SA%204.0-lightgrey.svg

Orginal [![cc0-1.0][cc0-1.0-shield]][cc0-1.0]
>
>COPYING: All the content within this repository is dedicated to the public domain under the CC0 1.0 Universal (CC0 1.0) Public Domain Dedication.
>
[![CC0-1.0][cc0-1.0-image]][cc0-1.0]

[cc0-1.0]: https://creativecommons.org/licenses/by/1.0/deed.en
[cc0-1.0-image]: https://licensebuttons.net/l/by/1.0/88x31.png
[cc0-1.0-shield]: https://img.shields.io/badge/License-CC0--1.0-lightgrey.svg

### Reference na licenciranje repozitorija
