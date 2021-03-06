﻿using Newtonsoft.Json.Linq;

namespace KenticoCloud.Delivery
{
    /// <summary>
    /// Represents a response from Kentico Cloud Delivery API that contains a content item.
    /// </summary>
    public sealed class DeliveryItemResponse
    {
        private readonly JToken _response;
        private readonly IDeliveryClient _client;
        private dynamic _modularContent;
        private ContentItem _item;

        /// <summary>
        /// Gets the content item from the response.
        /// </summary>
        public ContentItem Item
        {
            get { return _item ?? (_item = new ContentItem(_response["item"], _response["modular_content"], _client)); }
        }

        /// <summary>
        /// Gets the dynamic view of the JSON response where modular content items and their properties can be retrieved by name, for example <c>ModularContent.about_us.elements.description.value</c>.
        /// </summary>
        public dynamic ModularContent
        {
            get { return _modularContent ?? (_modularContent = JObject.Parse(_response["modular_content"].ToString())); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeliveryItemResponse"/> class with information from a response.
        /// </summary>
        /// <param name="response">A response from Kentico Cloud Delivery API that contains a content item.</param>
        /// <param name="client">The client that retrieved the content item.</param>
        internal DeliveryItemResponse(JToken response, IDeliveryClient client)
        {
            _response = response;
            _client = client;
        }

        /// <summary>
        /// Casts DeliveryItemResponse to its generic version.
        /// </summary>
        /// <typeparam name="T">Target type.</typeparam>
        public DeliveryItemResponse<T> CastTo<T>()
        {
            return new DeliveryItemResponse<T>(_response, _client);
        }
    }
}
