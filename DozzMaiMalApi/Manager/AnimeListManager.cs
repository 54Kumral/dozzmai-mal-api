﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DozzMaiMalApi.Entity.Essentials;
using DozzMaiMalApi.Entity.DTO;
using System.Xml;
using System.Net;

/*
    
    UPDATES MADE:
        
        // Update Description \\    // Update Date \\   // Updater (Use github or mal user name) \\

    -> File Created and Add method is ready for testing <-> 17.02.2016 : 17.43 +02.00 <-> Lolerji

*/

namespace DozzMaiMalApi.Manager
{
    public class AnimeListManager : IManager
    {
        private MalClient malClient;

        public AnimeListManager(MalClient client)
        {
            malClient = client;
        }

        #region IManager Implementation

        public void Add(IMalListEntity iMalEntity)
        {
            // If the user is authenticated
            if (malClient.User.IsAuthenticated)
            {
                // Get anime data as xml
                var memStreamXML = ManagerUtility.GenerateXMLData(iMalEntity);

                // If the user is authenicated
                if (malClient.User.IsAuthenticated)
                {
                    var anime = iMalEntity as DTOListAnime;
                    string queryString = ManagerUtility.GenerateQueryString(anime.ID);

                    // Upload data
                    malClient.WebClient.UploadData(queryString, memStreamXML.ToArray());
                }
            }
        }

        public void Delete(IMalListEntity iMalEntity)
        {
            throw new NotImplementedException();
        }

        public void Update(IMalListEntity iMalEntity)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
