// p³atnoœæ Stripe
window.redirectToCheckout = function (sessionId) {
    var stripe = Stripe("pk_test_51O2vOGKjb3t99QtDrtTTwBBXrQOYHWX6fVdaON8Yzz96N5oJitdBm7BEMu10bq72oNkuSWo8Dt7qXP23G5Pugrka00Bwk8lsyr");
    stripe.redirectToCheckout({ sessionId: sessionId });
}