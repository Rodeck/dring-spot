export interface MeetingPlace {
    id: number;
    name: string,
    text: string;
    latitude: number;
    longitude: number;
    categories: CategoryModel[];
    reviews: Review[],
    reviewsCount: number;
}

export interface CategoryModel {
    name: string;
}

export interface Review {
    id: number;
    date: Date;
    reviewer: string;
    points: number;
    text: string;
}