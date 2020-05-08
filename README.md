# Welcome to Walking Dinner

## Plan van aanpak
* We zijn begonnen met het opzoeken van tutorials over de technieken die we wilden gaan gebruiken ( ASP.NET Core, Razor )
* We hebben gezocht naar voorbeelden van een 'Walking Dinner' om een idee te krijgen van wat er verwacht werd.
* Opzet maken van pagina's die we verwachten nodig te hebben.
### Taakverdeling
We zijn begonnen met afzonderlijk van elkaar de front- en backend op te zetten, en daarna zijn we samen verder gegaan met de inhoud van elke pagina. We hebben, na de originele opzet, voornamelijk samen gewerkt door middel van voice chat, scherm delen en live share.

# Gebruikte technieken
We hebben ons project opgezet in ASP.NET Core, omdat dit moderner, cross-platform en dus klaar voor de toekomst is. We combineren het met Razor Pages zodat elke pagina een losse unit is, die één, herkenbare taak heeft. Dit vonden wij een fijne structuur geven.

![https://hackernoon.com/hn-images/1*XGFMlY2nwVoFOCUU6jpw-A.png](https://hackernoon.com/hn-images/1*XGFMlY2nwVoFOCUU6jpw-A.png)
# Opbouw en documentatie van code 
````mermaid
graph TD
A[Hoofdpagina]  --> B(Nieuw diner)
B --> C(Administrator krijgt een email met code)
B --> X[/Titel, beschrijving, verzamelpunt,<br />prijs, gegevens van aanmaker/]
C --> D(Beheerders spagina)
D --> E(Diner aanpassen)
D --> F(Gast uitnodigen)
F --> G(Gast krijgt email met code)
G --> H("Gast kan informatie invullen o.a.<br />#40;naam, adres, dieetwensen, plus 1&#41;")
D -->|Na uiterste inschrijfdatum| I(Administrator kiest gangen<br />op basis van aantal aanmeldingen)
I --> J(Server maakt verdeling, stuurt gasten email<br />met wanneer zij moeten koken en genereert<br />pdf met dinergegevens.)

# Werkt het eind-product
Ja.

# Manier van presenteren


