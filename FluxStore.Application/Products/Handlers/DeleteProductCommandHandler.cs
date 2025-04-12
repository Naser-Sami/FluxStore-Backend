using FluxStore.Application.Commands.Products.Commands;
using FluxStore.Application.Common;
using FluxStore.Application.Interfaces;
using MediatR;

namespace FluxStore.Application.Products.Handlers
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            await _productRepository.DeleteAsync(request.Id);
            return Result.Success("Product deleted successfully");
        }
    }
}