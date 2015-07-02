using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppCampus.Signboard.Components.Cache
{
    public class ImageCache
    {
        static ImageCache instance = new ImageCache();
        public static ImageCache Instance
        {
            get
            {
                return instance;
            }
        }

        private Hashtable hashtable = new Hashtable();
        private ImageCache() { }

        public void AddImage(Guid id, string base64Image)
        {
            if (hashtable.ContainsKey(id)) 
            {
                return;
            }

            hashtable.Add(id, base64Image);
        }

        public bool HasImage(Guid id)
        {
            return hashtable.ContainsKey(id);
        }

        public string GetImage(Guid id)
        {
            return (string) hashtable[id];
        }
    }
}
