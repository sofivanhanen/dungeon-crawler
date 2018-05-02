# Arkkitehtuuri

## Luokkakaavio

Päivitetty 2.5.2018. Kaavio on jonkin verran yksinkertaistettu.

Unity-kehityksessä on käytössä tavallisten C#-rakenteiden lisäksi GameObjecteja sekä Unityn omia MonoDevelop-skriptejä, jotka suoraan ohjaavat näitä objekteja.
Allaolevassa luokkakaaviossa MonoDevelop-skriptit ovat tyypiltään 'Controller'.
GameObjectit ovat ne oliot, jotka ruudulla näkyy, eli GameObjectit ja niiden kontrollerit vastaavat UI-puolesta.
Object-tyyppiset oliot vastaavat sovelluslogiikasta.
Ne ovat n.s. 'Behaviour Controllers'.
Tag-tyyppiset oliot viittaavat Unityn tag-systeemiin.
Näin GameObjectit voivat omalla tavallaan toteuttaa perintää: Esimerkiksi Ghost-oliolla on tagi Enemy, jolloin muut GameObjectit ja niiden kontrollerit voivat tunnistaa, että kyseinen GameObject oli Enemy.

![Luokkaaavio](class_diagram.png)

## Sekvenssikaavio

Kaavio kuvaa tasojen satunnaisgenerointia.
GameMasterController on MonoDevelop-scripti, eli se hoitaa työn UI-puolen, ja LevelGenerator on vastuussa koko logiikasta.

![Sekvenssikaavio](sequence_diagram.png)
