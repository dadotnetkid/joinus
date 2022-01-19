using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Fleet.Files.Tests;

public class FilesController_tests : IntegrationTest
{
    private byte[] file;

    [SetUp]
    public void SetUp()
    {
        file = File.ReadAllBytes(Path.Combine(TestContext.CurrentContext.WorkDirectory, "app_data",
            "1.jpg"));
    }
    [Test, Order(0)]
    public async Task Add_files_success()
    {
        var requestContent = new MultipartFormDataContent();
        var imageContent = new ByteArrayContent(file);
        imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
        requestContent.Add(imageContent, "file","1.jpg");

        var post= await _httpClient.PostAsync("api/files", requestContent);
        var res=await post.Content.ReadFromJsonAsync<Repository.Entities.Files>();
        Assert.AreEqual("1.jpg", res.Name);
    }
    [Test, Order(1)]
    public async Task Get_files_success()
    {
        var get= await _httpClient.GetFromJsonAsync<List<Repository.Entities.Files>>("api/files");
        Assert.NotNull(get);
    }
}