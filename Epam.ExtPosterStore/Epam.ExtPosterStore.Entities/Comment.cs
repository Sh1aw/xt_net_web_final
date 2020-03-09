using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.ExtPosterStore.Entities
{
    public class Comment
    {
        private string _text;
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public string Text
        {
            get => _text;
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("text of comment cant be empty or null");
                }

                _text = value;
            }
        }

        public int ProductId { get; set; }
        public int UserId { get; set; }

        public Comment(string text, int userId, int productId, DateTime creationTime)
        {
            Text = text;
            UserId = userId;
            ProductId = productId;
            CreationTime = creationTime;
        }
    }
}
