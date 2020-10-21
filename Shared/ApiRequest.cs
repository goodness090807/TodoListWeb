
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TodoListWeb.Models;
using TodoListWeb.ViewModels.Account;
using TodoListWeb.ViewModels.TodoList;

namespace TodoListWeb.Shared
{
    public class ApiRequest
    {
        private string PostToBackApi(string url, string method, string token, string postBody)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = method;
            request.ContentType = "application/json";


            if (!string.IsNullOrEmpty(postBody))
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(postBody);//要發送的字串轉為byte[]

                using (Stream reqStream = request.GetRequestStream())
                {
                    reqStream.Write(byteArray, 0, byteArray.Length);
                }
            }

            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Add("Authorization", $"Bearer {token}");
            }


            //發出Request
            string responseStr = string.Empty;


            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    responseStr = reader.ReadToEnd();
                }

            }

            return responseStr;
        }


        public AccountModel GetAccount(LoginViewModel loginViewModel)
        {
            string url = "https://apicorepractice.herokuapp.com/api/account/signin";

            string postBody = JsonConvert.SerializeObject(loginViewModel);//將匿名物件序列化為json字串

            string resData = PostToBackApi(url, "POST", string.Empty, postBody);

            if (string.IsNullOrEmpty(resData))
            {
                AccountModel accountModel = null;
                return accountModel;
            }

            return JsonConvert.DeserializeObject<AccountModel>(resData);
        }

        public AccountModel RegisterAccount(RegisterViewModel registerViewModel)
        {
            string url = "https://apicorepractice.herokuapp.com/api/userinfos";

            string postBody = JsonConvert.SerializeObject(registerViewModel);//將匿名物件序列化為json字串

            string resData = PostToBackApi(url, "POST", string.Empty, postBody);

            if (string.IsNullOrEmpty(resData))
            {
                AccountModel accountModel = null;
                return accountModel;
            }

            return JsonConvert.DeserializeObject<AccountModel>(resData);
        }

        public List<TodoListModel> GetTodoLists(string userId, string token)
        {
            string url = $"https://apicorepractice.herokuapp.com/api/userinfos/{userId}/todolists";

            string resData = PostToBackApi(url, "GET", token, string.Empty);

            if (string.IsNullOrEmpty(resData))
            {
                List<TodoListModel> todoListModel = null;
                return todoListModel;
            }

            return JsonConvert.DeserializeObject<List<TodoListModel>>(resData);
        }

        public TodoListModel GetTodoListById(string listId, string token)
        {
            string url = $"https://apicorepractice.herokuapp.com/api/todolists/{listId}";

            string resData = PostToBackApi(url, "GET", token, string.Empty);

            if (string.IsNullOrEmpty(resData))
            {
                TodoListModel todoListModel = null;
                return todoListModel;
            }

            return JsonConvert.DeserializeObject<TodoListModel>(resData);
        }

        public TodoListModel TodoCreate(TodoListsViewModel todoListsViewModel, string token)
        {
            string url = $"https://apicorepractice.herokuapp.com/api/todolists";

            string postBody = JsonConvert.SerializeObject(todoListsViewModel);//將匿名物件序列化為json字串

            string resData = PostToBackApi(url, "POST", token, postBody);

            if (string.IsNullOrEmpty(resData))
            {
                TodoListModel todoListModel = null;
                return todoListModel;
            }

            return JsonConvert.DeserializeObject<TodoListModel>(resData);
        }

        public TodoListModel TodoUpdate(string listId, TodoListsViewModel todoListsViewModel, string token)
        {
            string url = $"https://apicorepractice.herokuapp.com/api/todolists/{listId}";

            string postBody = JsonConvert.SerializeObject(todoListsViewModel);//將匿名物件序列化為json字串

            string resData = PostToBackApi(url, "PUT", token, postBody);

            if (string.IsNullOrEmpty(resData))
            {
                TodoListModel todoListModel = null;
                return todoListModel;
            }

            return JsonConvert.DeserializeObject<TodoListModel>(resData);
        }

        public bool TodDelete(string listId, string token)
        {
            string url = $"https://apicorepractice.herokuapp.com/api/todolists/{listId}";

            string resData = PostToBackApi(url, "DELETE", token, string.Empty);


            if (string.IsNullOrEmpty(resData))
            {
                return true;
            }

            return false;
        }

    }
}
