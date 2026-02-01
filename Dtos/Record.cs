namespace FirstProject.Dtos;

public record Record(
    int Id,
    string Name, 
    string Gmail,
    string Password
);


public record CreateUser(
    string Name, 
    string Gmail,
    string Password
);


public record UpdateUser(
    string Name, 
    string Gmail,
    string Password
);
