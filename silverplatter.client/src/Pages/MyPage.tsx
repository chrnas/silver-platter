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
        <div style={{height: "calc(100vh - 5rem)"}}>
            <div id="MyPageBody">
                <section className='FavoriteRestaurants'>
                    <h1>My Favorite Restaurants</h1>
                    <RestaurantButton restaurant={tempRestaurant}/>
                    <RestaurantButton restaurant={tempRestaurant}/>
                    <RestaurantButton restaurant={tempRestaurant}/>
                    <RestaurantButton restaurant={tempRestaurant}/>
                </section>

                <section className='Profile'>
                    <div id="ProfileHeader">
                        <img src="" alt="" />
                        <h1>Profile</h1>
                    </div>
                    <div id='ProfileSettings'>
                        <nav>
                            <input type="text" name="" id="" />
                            <input type="text" name="" id="" />
                            <input type="text" name="" id="" />
                            <input type="text" name="" id="" />
                        </nav>
                    </div>
                </section>
            </div>
        </div>
    )
}

export default MyPage;