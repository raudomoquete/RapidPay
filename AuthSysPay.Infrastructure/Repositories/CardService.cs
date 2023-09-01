using AuthSysPay.Core;

namespace AuthSysPay.Infrastructure
{
    public class CardService : ICardService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CardService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateCard(Card card)
        {
            await _unitOfWork.CardRepository.Add(card);
            await _unitOfWork.SaveChagesAsync();
        }

        public async Task<decimal> GetBalance(Card card)
        {
            var tmpCard = await _unitOfWork.CardRepository.GetById(card.Id);
            return tmpCard.Balance;
        }

        public async Task Pay(Card card, decimal amount)
        {
            var tmpCard = await _unitOfWork.CardRepository.GetById(card.Id);
            var fee = UniversalFeesExchange.Instance.GetCurrentFee();
            tmpCard.Balance -= amount + fee;
            _unitOfWork.CardRepository.Update(tmpCard);
            await _unitOfWork.SaveChagesAsync();
        }

        //public async Task UpdateBalance(Card card, decimal newBalance)
        //{
        //    var tmpCard = await _unitOfWork.CardRepository.GetById(card.Id);
        //    tmpCard.Balance = newBalance;
        //    _unitOfWork.CardRepository.Update(tmpCard);
        //    await _unitOfWork.SaveChagesAsync();
        //}
    }
}
