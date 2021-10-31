SELECT * FROM agendas."Speakers";
SELECT * FROM conferences."Conferences";
SELECT * FROM conferences."Hosts";
SELECT * FROM users."Users";
SELECT * FROM agendas."CallForPapers";
SELECT * FROM agendas."Submissions";
SELECT * FROM agendas."AgendaItems";
SELECT * FROM agendas."AgendaSlots";
SELECT * FROM agendas."AgendaTracks";
SELECT * FROM agendas."SpeakerSubmission";
SELECT * FROM agendas."AgendaItemSpeaker";
SELECT * FROM tickets."Conferences";
SELECT * FROM tickets."TicketSales";
SELECT * FROM tickets."Tickets";
SELECT * FROM speakers."Speakers";
SELECT * FROM users."Users";

-- Below query result have to be alway empty
-- select * from agendas."AgendaItems" a 
--     where a."Id" in (
--         select ai."Id" from agendas."Speakers" s
--             INNER join agendas."AgendaItemSpeaker" ais
--                 on s."Id" = ais."SpeakersId"
--             RIGHT join agendas."AgendaItems" ai
--                 on ai."Id" = ais."AgendaItemsId"
--             where s."Id" is null
--                 );

-- Below query result have to be alway empty
-- select * from agendas."Submissions" s 
--     where s."Id" in (
--         select ai."Id" from agendas."Speakers" s
--             INNER join agendas."AgendaItemSpeaker" ais
--                 on s."Id" = ais."SpeakersId"
--             RIGHT join agendas."AgendaItems" ai
--                 on ai."Id" = ais."AgendaItemsId"
--             where s."Id" is null
--                 );
