using OPSExporter.Data.Entity;
using OPSExporter.Data.Repository;
using OPSExporter.Xlsx;

Writer writer = new Writer("Test.xlsx");

writer.Init();

if (writer.IsOpened()) {
    writer.Sheet("1 лист").Row();
    
    writer.Write("Например пизда").Write("А это говно").Write("И это пизда").Row();

    NodesRepository nodesRepository = new NodesRepository();

    List<IGrouping<string?, Node>> nodes = await nodesRepository.GetAll();

    if (nodes.Count != 0) {
        foreach (IGrouping<string?, Node> nodeGroup in nodes.Where(nodeGroup => nodeGroup.Any())) {
            writer.Write(nodeGroup.Key?.Normalize());
                
            foreach (Node node in nodeGroup) {
                writer.Write(node.ValueDouble).Write(node.ValueInteger).Write(node.ActualTime).Row();
            }
        }
    }
}