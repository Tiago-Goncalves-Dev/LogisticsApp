
export interface CreateOrderDto {
    CustomerId: number;
    deliveryType: deliveryType;
    pickupAddress: string;
    homedeliveryAddress: string;
    WeightKg: number;
    plannedDeliveryDate: Date;
}

export interface OrderInfoDto{
    orderNumber?: string;
    customerId: number;
    deliveryType: deliveryType;
    pickupAddress?: string;
    homeDeliveryAddress?: string;
    weightKg: number;
    status: OrderStatus;
    createdAt: Date;
    plannedDeliveryDate?: Date; 
}

export enum OrderStatus {
    Created = 'Created',
    OnGoing = 'Ongoing',
    Finished = 'Finished',
}

export enum deliveryType {
    Pickup = 'Pickup',
    HomeDelivery = 'HomeDelivery',
}


