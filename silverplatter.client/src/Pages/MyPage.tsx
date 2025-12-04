import RestaurantButton from '../Components/RestaurantButton';
import './css/MyPage.css'

function MyPage() {
    let tempRestaurant = {
        Id: 0,
        Name: "Temporary Restaurant",
        Description: "Discover the best temp in temp, all at the small price of 12.99 Temp, you couldn't dream of a better dream than that!",
        Address: "tempytemptemptemp"
    }

    return (
        <div>
            <div className='FavoriteRestaurants'>
                <RestaurantButton restaurant={tempRestaurant}/>
            </div>
        </div>
    )
}

export default MyPage;