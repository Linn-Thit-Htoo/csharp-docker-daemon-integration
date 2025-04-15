namespace PublicApi.Models;

public class DockerImageModel
{
    public ImageModel[] Images { get; set; }
}

public class ImageModel
{
    public int Containers { get; set; }
    public int Created { get; set; }
    public string Id { get; set; }
    public Labels Labels { get; set; }
    public string ParentId { get; set; }
    public string[] RepoDigests { get; set; }
    public string[] RepoTags { get; set; }
    public int SharedSize { get; set; }
    public int Size { get; set; }
}

public class Labels
{
    public string comdockercomposeproject { get; set; }
    public string comdockercomposeservice { get; set; }
    public string comdockercomposeversion { get; set; }
    public string maintainer { get; set; }
}
