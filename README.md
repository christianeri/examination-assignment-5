examination-assignment-5

Innehåller:

Konsollapplikation "Support Ticket Manager" (.NET 7, Entity Framework Core)
- Direct mapping
- UI: Tangentnavigerad

Funktionalitet:

Data sparas/hämtas från fyra tabeller lagrade i lokal SQL Express databas. "Tickets" är kopplad till "under"-tabellerna "Customers", "Buildings" och "Statuses" via FKs.
Tabeller normaliserade 1-3NF. *Funderade och gjorde bedömningen att kolumnerna "kommentar" (samt tillhörande tidsangivelse) potentiellt kan användas för att på ett meningsfullt identifiera specifik entry och fick därför ligga i "Ticket"-tabellen... 

- Skapa nytt felanmälningsärende som lagrar
	- Beskrivning
	- Byggnad där kunden upptäckt felet (pre-seeded data i egen tabell).
	- Tidpunkt då ärendet skapades
	- Kundinformation
         
- Visa alla ärenden
- Visa specifikt ärende
- Radera specifikt ärende med kontrollfråga
- Uppdatera specifikt ärende
	- Beskrivning
	- Kundinformation
	- Ärendestatus
	- Kommentar med tidsangivelse för senaste uppdatering    
- Avsluta programmet
