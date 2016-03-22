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
    -> Query method created                 <-> 18.02.2016 : 19.56 +02.00 <-> Lolerji
    -> Generate Query string method updated <-> 24.02.2016 : 20.52 +02.00 <-> Lolerji
       The method can now update, add
       and delete anime data to/from
       user's list

*/


namespace DozzMaiMalApi.Manager
{
    [Obsolete("This class's functionality has been replaced by the MALQuery classes. Use the query class of interest instead of using this class")]
    public static class ManagerUtility
    {
        [Obsolete("This method has been depraced! Use the GenerateXMLData method of MALQuery objects instead", false)]
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
                var xml = new StringBuilder();
                xml.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                xml.AppendLine("<entry>");
                xml.AppendLine($"<episode>{anime.Episode}</episode>");
                xml.AppendLine($"<status>{anime.Status}</status>");
                xml.AppendLine($"<score>{anime.Score}</score>");
                //xml.AppendLine($"<download_episodes>{anime.Downloaded}</download_episodes>");
                //xml.AppendLine($"<storage_type>{anime.StorageType}</storage_type>");
                //xml.AppendLine($"<storage_value>{anime.StorageValue}</storage_value>");
                //xml.AppendLine($"<times_rewatched>{anime.TimesRewRer}</times_rewatched>");
                //xml.AppendLine($"<rewatch_value>{anime.RewRerValue}</rewatch_value>");
                //xml.AppendLine($"<date_start>{anime.DateStarted}</date_start>");
                //xml.AppendLine($"<date_finish>{anime.DateFinished}</date_finish>");
                //xml.AppendLine($"<priority>{anime.Priority}</priority>");
                //xml.AppendLine($"<enable_discussion>{anime.EnableDisscussion}</enable_discussion>");
                //xml.AppendLine($"<enable_rewatching>{anime.EnableRewRer}</enable_rewatching>");
                //xml.AppendLine($"<comments>{anime.Comments}</comments>");
                //xml.AppendLine($"<fansub_group>{anime.Group}</fansub_group>");
                //xml.AppendLine($"<tags>{anime.Tags}</tags>");
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

        [Obsolete("This method has been deprecated! Use MALQuery classes and their query methods instead", false)]
        public static async Task<string> Query(string queryString, MalClient client)
        {
            var resp = await client.HttpClient.GetStreamAsync(queryString);

            // Get response string
            var reader = new StreamReader(resp);
            var respString = reader.ReadToEnd();

            return respString;
        }

        // Generate query string methoduna get anime list methodunu uygula
        [Obsolete("This method has been deprecated! Use MALQuery derived classes (MALAddQuery, MALUpdateQuery... etc.) instead.",false)]
        public static string GenerateQueryString(int id, StringBuilder xmlData, Common.ApiMethods apiMethod)
        {
            var method = apiMethod.ToString().ToLower();
            var queryString = Uri.EscapeUriString(apiMethod == Common.ApiMethods.Get ?
                                                        $"http://myanimelist.net/animelist/lolerji"
                                                        : apiMethod != Common.ApiMethods.Delete ?
                                                                $"http://myanimelist.net/api/animelist/{method}/{id}.xml?data={xmlData}"
                                                                : $"http://myanimelist.net/api/animelist/{method}/{id}.xml");
            return queryString;
        }
    }
}
