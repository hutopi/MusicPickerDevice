// ***********************************************************************
// Assembly         : MusicPickerDeviceApp
// Author           : Pierre
// Created          : 06-20-2015
//
// Last Modified By : Pierre
// Last Modified On : 06-21-2015
// ***********************************************************************
// <copyright file="ApiClient.cs" company="Hutopi">
//     Copyright ©  2015 Hugo Caille, Pierre Defache & Thomas Fossati.
//     Music Picker is released upon the terms of the Apache 2.0 License.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MusicPickerDeviceApp.App.Api.Util;
using Newtonsoft.Json;

/// <summary>
/// The App namespace.
/// </summary>
namespace MusicPickerDeviceApp.App
{
    /// <summary>
    /// Class that represents the Api client.
    /// </summary>
    public class ApiClient
    {
        /// <summary>
        /// The Uri of the werbservice
        /// </summary>
        private Uri endpoint;
        /// <summary>
        /// The bearer token
        /// </summary>
        private string bearer;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiClient"/> class.
        /// </summary>
        /// <param name="endpoint">The endpoint.</param>
        public ApiClient(Uri endpoint)
        {
            this.endpoint = endpoint;
        }

        /// <summary>
        /// Signs up.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool SignUp(string username, string password)
        {
            Uri uri = new Uri(endpoint, "/api/Account/Register");
            HttpContent content = new FormUrlEncodedContent(new []
            {
                new KeyValuePair<string, string>("Username", username),
                new KeyValuePair<string, string>("Password", password),
                new KeyValuePair<string, string>("ConfirmPassword", password), 
            });

            HttpResponseMessage result = (new HttpClient()).PostAsync(uri, content).Result;

            if (result.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Retrieves the bearer.
        /// </summary>
        /// <returns>System.String.</returns>
        public string RetrieveBearer()
        {
            return this.bearer;
        }

        /// <summary>
        /// Provides the bearer.
        /// </summary>
        /// <param name="bearer">The bearer.</param>
        public void ProvideBearer(string bearer)
        {
            this.bearer = bearer;
        }

        /// <summary>
        /// Logs in the user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool LogIn(string username, string password)
        {
            Uri uri = new Uri(endpoint, "/oauth/token");
            HttpContent content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password),
                new KeyValuePair<string, string>("grant_type", "password")
            });

            HttpResponseMessage result = (new HttpClient()).PostAsync(uri, content).Result;
            if (!result.IsSuccessStatusCode)
            {
                return false;
            }

            Dictionary<string, string> data =
                JsonConvert.DeserializeObject<Dictionary<string, string>>(result.Content.ReadAsStringAsync().Result);
           
            ProvideBearer(data["access_token"]);
            return true;
        }

        /// <summary>
        /// Add a device to the webservice.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>System.Int32.</returns>
        public int DeviceAdd(string name)
        {
            Uri uri = new Uri(endpoint, "/api/Devices");
            HttpContent content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("name", name),
            });

            HttpResponseMessage result = (new HttpClient()
            {
                DefaultRequestHeaders = { Authorization = new AuthenticationHeaderValue("Bearer", this.bearer)}
            }).PostAsync(uri, content).Result;
            if (!result.IsSuccessStatusCode)
            {
                return -1;
            }

            Dictionary<string, string> data =
               JsonConvert.DeserializeObject<Dictionary<string, string>>(result.Content.ReadAsStringAsync().Result);

            return Convert.ToInt32(data["Id"]);
        }

        /// <summary>
        /// Delete the device
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool DeviceDelete(int deviceId)
        {
            Uri uri = new Uri(endpoint, string.Format("/api/Devices/{0}", deviceId));

            HttpResponseMessage result = (new HttpClient()
            {
                DefaultRequestHeaders = { Authorization = new AuthenticationHeaderValue("Bearer", this.bearer) }
            }).DeleteAsync(uri).Result;
            if (!result.IsSuccessStatusCode)
            {
                return false; 
            }

            return true;
        }

        /// <summary>
        /// Submit the device collection to the webservice
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <param name="collection">The collection.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        public async Task<bool> DeviceCollectionSubmit(int deviceId, string collection)
        {
            Uri uri = new Uri(endpoint, string.Format("/api/Devices/{0}/Submit", deviceId));

            HttpContent content = new StringContent(collection)
            {
                Headers = { ContentType = new MediaTypeHeaderValue("application/json")}
            };

            HttpResponseMessage result = await (new HttpClient()
            {
                DefaultRequestHeaders = { Authorization = new AuthenticationHeaderValue("Bearer", this.bearer) }
            }).PostAsync(uri, content);

            if (!result.IsSuccessStatusCode)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Get the devices of the connected user
        /// </summary>
        /// <returns>List&lt;Device&gt;.</returns>
        public List<Device> DevicesGet()
        {
            Uri uri = new Uri(endpoint, "/api/Devices");

            HttpResponseMessage result = (new HttpClient()
            {
                DefaultRequestHeaders = { Authorization = new AuthenticationHeaderValue("Bearer", this.bearer) }
            }).GetAsync(uri).Result;

            if (!result.IsSuccessStatusCode)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<List<Device>>(result.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// Get the device ID by its name
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>System.Int32.</returns>
        public int DeviceGetIdByName(string name)
        {
            Uri uri = new Uri(endpoint, string.Format("/api/Devices?name={0}", name));
            HttpResponseMessage result = (new HttpClient()
            {
                DefaultRequestHeaders = { Authorization = new AuthenticationHeaderValue("Bearer", this.bearer) }
            }).GetAsync(uri).Result;

            if (!result.IsSuccessStatusCode)
            {
                return -1;
            }

            return JsonConvert.DeserializeObject<Device>(result.Content.ReadAsStringAsync().Result).Id;
        }

        /// <summary>
        /// Get the album from its ID from the current device
        /// </summary>
        /// <param name="albumId">The album identifier.</param>
        /// <returns>Album.</returns>
        public Album DevicesGetAlbum(int albumId)
        {
            Uri uri = new Uri(endpoint, string.Format("/api/Albums/{0}", albumId));

            HttpResponseMessage result = (new HttpClient()
            {
                DefaultRequestHeaders = { Authorization = new AuthenticationHeaderValue("Bearer", this.bearer) }
            }).GetAsync(uri).Result;

            if (!result.IsSuccessStatusCode)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<Album>(result.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// Get the albums of the device.
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <returns>List&lt;Album&gt;.</returns>
        public List<Album> DeviceGetAlbums(int deviceId)
        {
            Uri uri = new Uri(endpoint, string.Format("/api/Albums?device={0}", deviceId));

            HttpResponseMessage result = (new HttpClient()
            {
                DefaultRequestHeaders = { Authorization = new AuthenticationHeaderValue("Bearer", this.bearer) }
            }).GetAsync(uri).Result;

            if (!result.IsSuccessStatusCode)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<List<Album>>(result.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// Get the albums of an artist from the device
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <param name="artist">The artist.</param>
        /// <returns>List&lt;Album&gt;.</returns>
        public List<Album> DeviceGetAlbumsFromArtist(int deviceId, string artist)
        {
            Uri uri = new Uri(endpoint, string.Format("/api/Albums?device={0}&artist={1}", deviceId, artist));

            HttpResponseMessage result = (new HttpClient()
            {
                DefaultRequestHeaders = { Authorization = new AuthenticationHeaderValue("Bearer", this.bearer) }
            }).GetAsync(uri).Result;

            if (!result.IsSuccessStatusCode)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<List<Album>>(result.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// Get the tracks of the device
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <returns>List&lt;Track&gt;.</returns>
        public List<Track> DeviceGetTracks(int deviceId)
        {
            Uri uri = new Uri(endpoint, string.Format("/api/Tracks?device={0}", deviceId));
            HttpResponseMessage result = (new HttpClient()
            {
                DefaultRequestHeaders = { Authorization = new AuthenticationHeaderValue("Bearer", this.bearer) }
            }).GetAsync(uri).Result;

            if (!result.IsSuccessStatusCode)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<List<Track>>(result.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// Get the track which has trackId as Id
        /// </summary>
        /// <param name="trackId">The track identifier.</param>
        /// <returns>Track.</returns>
        public Track DevicesGetTrack(int trackId)
        {
            Uri uri = new Uri(endpoint, string.Format("/api/Tracks/{0}", trackId));
            HttpResponseMessage result = (new HttpClient()
            {
                DefaultRequestHeaders = { Authorization = new AuthenticationHeaderValue("Bearer", this.bearer) }
            }).GetAsync(uri).Result;

            if (!result.IsSuccessStatusCode)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<Track>(result.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// Get the tracks from an album
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <param name="albumId">The album identifier.</param>
        /// <returns>List&lt;Track&gt;.</returns>
        public List<Track> DeviceGetTracksFromAlbum(int deviceId, int albumId)
        {
            Uri uri = new Uri(endpoint, string.Format("/api/Tracks?device={0}&album={1}}", deviceId, albumId));
            HttpResponseMessage result = (new HttpClient()
            {
                DefaultRequestHeaders = { Authorization = new AuthenticationHeaderValue("Bearer", this.bearer) }
            }).GetAsync(uri).Result;

            if (!result.IsSuccessStatusCode)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<List<Track>>(result.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// Get the artist who has artistId as id
        /// </summary>
        /// <param name="artistId">The artist identifier.</param>
        /// <returns>Artist.</returns>
        public Artist DevicesGetArtist(int artistId)
        {
            Uri uri = new Uri(endpoint, string.Format("/api/Artists/{0}", artistId));
            HttpResponseMessage result = (new HttpClient()
            {
                DefaultRequestHeaders = { Authorization = new AuthenticationHeaderValue("Bearer", this.bearer) }
            }).GetAsync(uri).Result;

            if (!result.IsSuccessStatusCode)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<Artist>(result.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// Get the artists of a device
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <returns>List&lt;Artist&gt;.</returns>
        public List<Artist> DeviceGetArtists(int deviceId)
        {
            Uri uri = new Uri(endpoint, string.Format("/api/Artists?device={0}", deviceId));
            HttpResponseMessage result = (new HttpClient()
            {
                DefaultRequestHeaders = { Authorization = new AuthenticationHeaderValue("Bearer", this.bearer) }
            }).GetAsync(uri).Result;

            if (!result.IsSuccessStatusCode)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<List<Artist>>(result.Content.ReadAsStringAsync().Result);
        } 

    }
}
