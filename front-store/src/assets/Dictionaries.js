const dictionary = {
  deliveryMethod: {
    StorePickup: 'Odbiór w sklepie',
    Courier: 'Kurier',
    Post: 'Poczta',
  },
  paymentMethods: {
    OnDelivery: 'Przy odbiorze',
    PaymentProvider: 'Płatność online',
  },
};
export default {
  translate(dictionaryName, value) {
    return dictionary[dictionaryName][value];
  },
};
