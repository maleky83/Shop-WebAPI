using MediatR;

public record UploadProductImageCommand(
    int ProductId,
    Stream FileStream,
    string FileName,
    long FileSize
) : IRequest<string>;