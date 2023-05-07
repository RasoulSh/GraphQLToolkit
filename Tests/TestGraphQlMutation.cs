using System;
using System.Collections;
using GraphQLToolkit.Mutation;
using GraphQLToolkit.Query;
using UnityEngine;
using UnityEngine.Networking;

namespace Tests
{
    public class TestGraphQlMutation : MonoBehaviour
    {
        [SerializeField] private string url = "http://3.68.167.173/graphql";
        [SerializeField] private string token = "8|aINi4oEWCwf8rShgnelfd7XB3QFEAH6zAHrcZL7t";
        [SerializeField] private string mutationName = "UpdateUser";
        [SerializeField] private string methodName = "updateUser";
        [SerializeField] private int userId = 2;
        [SerializeField] private string newTeamName = "testGQTNewTeamName";

        [ContextMenu("Send")]
        private void Send()
        {
            StartCoroutine(SendRoutine());
        }

        private IEnumerator SendRoutine()
        {
            var responseQuery = new GraphQlQuery();
            responseQuery.Add("user").Add("team_name");
            var arguments = new GraphQlMutationArgument[]
            {
                new("id", userId),
                new("team_name", newTeamName)
            };
            var mutation = new GraphQlMutation(mutationName, methodName, arguments, responseQuery);
            var mutationStr = mutation.ToString();

            using var request = new UnityWebRequest(url, "Post");
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Authorization", $"Bearer {token}");
            request.SetRequestHeader("Content-Type", "application/json");
            request.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(mutationStr));
            request.SendWebRequest();
            while (!request.isDone)
                yield return null;
            if (request.result != UnityWebRequest.Result.Success || request.responseCode != 200)
            {
                Debug.LogError($"error request: {request.responseCode}");
            }
            else
            {
                Debug.Log($"success: {request.downloadHandler.text}");
            }
        }
    }
}