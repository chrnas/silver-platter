import PageButton from "../Components/PageButton";
import PageLogo from "../Components/PageLogo";
import './css/Header.css'

function Header() {
    return (
        <div className="Header">
            <PageLogo />
            <nav className="HeaderItems">
                <PageButton name="Home" ref="/" />
                <PageButton name="Browse" ref="/Browse" />
                <PageButton name="My Page" ref="/MyPage" />
            </nav>
                <PageButton name="Testing space" ref="/TestingPage"/>
            </div>
        </div>
    )
}

export default Header;