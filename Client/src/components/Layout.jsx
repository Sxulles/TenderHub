import NavMenu from "./NavMenu";

const Layout = (props) => {
    return (
        <div>
            <NavMenu />
            <div tag="main">
                {props.children}
            </div>
        </div>
    );
}
 
export default Layout;