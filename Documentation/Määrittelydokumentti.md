# Määrittelydokumentti
### OTMK18 / Sofia Vanhanen
**Päivitetty 27.3.**

##### Yleiskuva
Unitylla (C#) toteutettu Diablo-tyylinen dungeon crawler peli. 3d, top-down, voxel graphics.
Satunnaisgeneroidut tasot.
Inspiraationa ehkä Diabloakin enemmän Fate.
Siis, tasot tietyn kokoisia, tavoite löytää rappuset jolla pääsee eteenpäin, tasoja mahdollisesti rajattomasti.
Korkeammilla tasoilla hirviöt ovat vahvempia, mutta myös palkinnot parempia.
Palkinnot voi viitata hirviöiden pudottamiin aarteisiin tai jonkinlaisiin pisteisiin (rahaa, kokemuspisteitä, tms.)

##### Perustoiminnallisuudet
- Pelaaja ohjaa reaaliajassa pelihahmoa 3d top-down ympäristössä
- Pelaaja taistelee hirviöitä vastaan miekkansa avulla
- Pelaaja navigoi satunnaisgeneroiduissa tasoissa ja yrittää löytää rappusia, joiden avulla pääsee seuraaville tasoille
 - Seuraava taso on aina edellistä vaikeampi ja palkitsevampi
- Jonkinlainen palkitsemissysteemi
- Jos pelihahmo kuolee, peli päättyy

##### Jatkokehitysideat
- Pelaajalle näkyvä tason minimap, joka laajenee sitä mukaa kuin pelaaja tutkii tasoa
- Loot system
 - Hirviöt pudottavat kuollessaan kultaa, aseita ja varusteita
 - Pelihahmo voi käyttää aseita ja varusteita, joita pelin aikana löytää
 - Tietyiltä tasoilta löytyy myyntimies, jolle pelaaja voi myyydä tarpeettomat aarteet, ja jolta voi ostaa esim. taikajuomia
- Leveling system
 - Pelaaja kerää kokemusta (exp points)
 - Tarpeeksi pisteitä saatuaan pelihahmo saavuttaa uuden kokemustason jolloin hahmon attribuutit paranevat
 - Uudella tasolla pelaaja saa käyttöönsä pisteitä, joilla pelaaja voi itse kehittää hahmoaan esim. uusien loitsujen oppimiseen
- Loitsut
 - Pelaaja voi käyttää regeneroituvaa resurssia, manaa, loitsimiseen
 - Taisteluloitsuja, parantamisloitsu, teleport-loitsu, yms.
- Checkpoints
 - Pelaaja pääsee tallentamaan oman edistymisensä, ja kuoleman sattuessa pääsee jatkamaan tietystä pisteestä
- End game scenario
