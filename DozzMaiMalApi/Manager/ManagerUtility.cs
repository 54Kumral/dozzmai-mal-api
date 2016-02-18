﻿using DozzMaiMalApi.Entity.DTO;
using DozzMaiMalApi.Entity.Essentials;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


/*
    
    UPDATES MADE:
        
        // Update Description \\    // Update Date \\   // Updater (Use github or mal user name) \\

    -> File and class created code added    <-> 17.02.2016 : 17.32 +02.00 <-> Lolerji

*/


namespace DozzMaiMalApi.Manager
{
    public static class ManagerUtility
    {
        public static StringBuilder GenerateXMLData(IMalListEntity iMalEntity)
        {
            // Convert the interface object to a dto anime object
            var anime = iMalEntity as DTOListAnime;

            // Here we should convert the anime data to xml fromat to
            // dispatch to myanimelist, MAL only accepts xml format data <.<
            var settings = new XmlWriterSettings();
            settings.Indent = true;             // We want indentation in our xml!!!
            settings.CheckCharacters = true;    // 

            try
            {
                // Create memory stream - The xml will be written here
                //var memStream = new MemoryStream();
                //var xml = XmlWriter.Create(memStream, settings);

                //// Begin the file
                //xml.WriteStartDocument();


                //// Write anime data
                //xml.WriteStartElement("entry");

                //// Populate xml element
                //xml.WriteElementString("episode", anime.Episode.ToString());
                //xml.WriteElementString("status", anime.Status.ToString());
                //xml.WriteElementString("score", anime.Score.ToString());
                //xml.WriteElementString("downloaded_episodes", anime.Downloaded.ToString());
                //xml.WriteElementString("storage_type", anime.StorageType.ToString());
                //xml.WriteElementString("storage_value", anime.StorageValue.ToString());
                //xml.WriteElementString("times_rewatched", anime.TimesRewRer.ToString());
                //xml.WriteElementString("rewatch_value", anime.RewRerValue.ToString());
                //xml.WriteElementString("date_start", anime.DateStarted.ToShortDateString());
                //xml.WriteElementString("date_finish", anime.DateFinished.ToShortDateString());
                //xml.WriteElementString("priority", anime.Priority.ToString());
                //xml.WriteElementString("enable_discussion", anime.EnableDisscussion.ToString());
                //xml.WriteElementString("enable_rewatching", anime.EnableRewRer.ToString());
                //xml.WriteElementString("fansub_group", anime.Group);
                //xml.WriteElementString("tags", anime.Tags);

                var xml = new StringBuilder();
                xml.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                xml.AppendLine("<entry>");
                xml.AppendLine($"<episode>{anime.Episode}</episode>");
                xml.AppendLine($"<status>{anime.Status}</status>");
                xml.AppendLine($"<score>{anime.Score}</score>");
                xml.AppendLine($"<download_episodes>{anime.Downloaded}</download_episodes>");
                xml.AppendLine($"<storage_type>{anime.StorageType}</storage_type>");
                xml.AppendLine($"<storage_value>{anime.StorageValue}</storage_value>");
                xml.AppendLine($"<times_rewatched>{anime.TimesRewRer}</times_rewatched>");
                xml.AppendLine($"<rewatch_value>{anime.RewRerValue}</rewatch_value>");
                xml.AppendLine($"<date_start>{anime.DateStarted}</date_start>");
                xml.AppendLine($"<date_finish>{anime.DateFinished}</date_finish>");
                xml.AppendLine($"<priority>{anime.Priority}</priority>");
                xml.AppendLine($"<enable_discussion>{anime.EnableDisscussion}</enable_discussion>");
                xml.AppendLine($"<enable_rewatching>{anime.EnableRewRer}</enable_rewatching>");
                xml.AppendLine($"<comments>{anime.Comments}</comments>");
                xml.AppendLine($"<fansub_group>{anime.Group}</fansub_group>");
                xml.AppendLine($"<tags>{anime.Tags}</tags>");
                xml.AppendLine("</entry>");

                // Return the memory stream that holds the anime data
                return xml;
            }
            catch (XmlException xmlEx)
            {
                Debug.WriteLine("XmlException: " + xmlEx.Message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception: " + ex.Message);
            }

            // Return null if exceptions occur...
            return null;
        }

        public static string GenerateQueryString(int id, StringBuilder xmlData)
        {
            var queryString = Uri.EscapeUriString($"http://myanimelist.net/api/animelist/add/{id}.xml?data={xmlData}");
            return queryString;
        }
    }
}