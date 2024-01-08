import Link from "next/link";
import Logo from "./logo";

const NavLinks = {
  employer: [
    {
      name: "Home",
      link: "/employer",
    },
    {
      name: "Employer",
      link: "/employer",
    },
    {
      name: "Project",
      link: "/employer/project",
    },
  ],
  employee: [
    {
      name: "Home",
      link: "/employee",
    },
    {
      name: "Project",
      link: "/employee/project",
    },
  ],
};

let userRole = "employer";

const navLinks =
  userRole === "employer" ? NavLinks.employer : NavLinks.employee;
const Navbar = () => {
  return (
    // <div className="  flex  items-center p-4 bg-white">
    //   <div className="p-4  ">
    //     <Logo />
    //   </div>

    //   <div className="space-x-4 ">
    //     <a
    //       href="#"
    //       className=" bg-color_hover rounded-full px-2 py-2 m-2 hover:text-text_secondary font-sm hover:text-black text-text_secondary"
    //     >
    //       Home
    //     </a>
    //     <a href="#" className="text-text_tertiary  font-sm">
    //       Employee
    //     </a>
    //     <a href="#" className="text-text_tertiary font-sm">
    //       Project
    //     </a>
    //     <a
    //       href="#"
    //       className=" border rounded-full  bg-text_secondary  text-white  font-sm px-3 py-2 hover:bg-color_hover hover:text-text_tertiary"
    //     >
    //       {" "}
    //       FO
    //     </a>
    //   </div>
    // </div>
    <div className="flex items-center justify-between p-4 bg-white">
      <div className="p-4  ">
        <Logo />
      </div>
      <div>
        {navLinks.map((link) => (
          <Link
            href={link.link}
            key={link.name}
            className=" bg-color_hover rounded-full px-2 py-2 m-2  font-sm  text-text_tertiary hover:text_secondary"
          >
            {link.name}
          </Link>
        ))}
      </div>
      <div>
        {userRole === "employer" && (
          <div className="border-2 outline  border-red-500">
            <button className="bg-text_secondary p-2 m-2   text-white rounded-full">
              FO
            </button>
          </div>
        )}
      </div>
    </div>
  );
};

export default Navbar;
