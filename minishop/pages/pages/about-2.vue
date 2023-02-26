<template>
  <main class="main">
    <div
      class="page-header text-center"
      v-lazy:background-image="'./images/page-header-bg.jpg'"
    >
      <div class="container">
        <h1 class="page-title">
          About us 2
          <span>Pages</span>
        </h1>
      </div>
    </div>

    <div class="page-content pb-3">
      <div class="container">
        <div class="row">
          <div class="col-lg-10 offset-lg-1">
            <div class="about-text text-center mt-3">
              <h2 class="title text-center mb-2">Who We Are</h2>

              <p>
                Sed pretium, ligula sollicitudin laoreet viverra, tortor libero
                sodales leo, eget blandit nunc tortor eu nibh. Suspendisse
                potenti. Sed egestas, ante et vulputate volutpat, uctus metus
                libero eu augue. Morbi purus libero, faucibus adipiscing,
                commodo quis, gravida id, est. Sed lectus. Praesent elementum
                hendrerit tortor. Sed semper lorem at felis.
              </p>
              <img
                v-lazy="'./images/about/about-2/signature.png'"
                alt="signature"
                class="mx-auto mb-5 bg-transparent"
              />

              <img
                v-lazy="'./images/about/about-2/img-1.jpg'"
                alt="image"
                class="mx-auto mb-6"
              />
            </div>
          </div>
        </div>

        <div class="row justify-content-center">
          <div class="col-lg-4 col-sm-6">
            <div class="icon-box icon-box-sm text-center">
              <span class="icon-box-icon">
                <i class="icon-puzzle-piece"></i>
              </span>
              <div class="icon-box-content">
                <h3 class="icon-box-title">Design Quality</h3>

                <p>
                  Sed egestas, ante et vulputate volutpat, eros pede semper est,
                  vitae luctus metus libero
                  <br />eu augue.
                </p>
              </div>
            </div>
          </div>

          <div class="col-lg-4 col-sm-6">
            <div class="icon-box icon-box-sm text-center">
              <span class="icon-box-icon">
                <i class="icon-life-ring"></i>
              </span>
              <div class="icon-box-content">
                <h3 class="icon-box-title">Professional Support</h3>

                <p>
                  Praesent dapibus, neque id cursus faucibus,
                  <br />tortor neque egestas augue, eu vulputate <br />magna
                  eros eu erat.
                </p>
              </div>
            </div>
          </div>

          <div class="col-lg-4 col-sm-6">
            <div class="icon-box icon-box-sm text-center">
              <span class="icon-box-icon">
                <i class="icon-heart-o"></i>
              </span>
              <div class="icon-box-content">
                <h3 class="icon-box-title">Made With Love</h3>

                <p>
                  Pellentesque a diam sit amet mi ullamcorper
                  <br />vehicula. Nullam quis massa sit amet <br />nibh viverra
                  malesuada.
                </p>
              </div>
            </div>
          </div>
        </div>
      </div>

      <div class="mb-2"></div>

      <div
        class="bg-image pt-7 pb-5 pt-md-12 pb-md-9"
        v-lazy:background-image="'./images/backgrounds/bg-4.jpg'"
      >
        <div class="container">
          <div class="row">
            <div class="col-6 col-md-3">
              <div class="count-container text-center">
                <count-to
                  :from="0"
                  :to="40"
                  :speed="3000"
                  :interval="50"
                  class="text-white"
                  >K+</count-to
                >

                <h3 class="count-title text-white">Happy Customer</h3>
              </div>
            </div>

            <div class="col-6 col-md-3">
              <div class="count-container text-center">
                <count-to
                  :from="0"
                  :to="20"
                  :speed="3000"
                  :interval="50"
                  class="text-white"
                  >+</count-to
                >

                <h3 class="count-title text-white">Years in Business</h3>
              </div>
            </div>

            <div class="col-6 col-md-3">
              <div class="count-container text-center">
                <count-to
                  :from="0"
                  :to="95"
                  :speed="3000"
                  :interval="50"
                  class="text-white"
                  >%</count-to
                >

                <h3 class="count-title text-white">Return Clients</h3>
              </div>
            </div>

            <div class="col-6 col-md-3">
              <div class="count-container text-center">
                <count-to
                  :from="0"
                  :to="15"
                  :speed="3000"
                  :interval="50"
                  class="text-white"
                ></count-to>

                <h3 class="count-title text-white">Awards Won</h3>
              </div>
            </div>
          </div>
        </div>
      </div>

      <div class="bg-light-2 pt-6 pb-7 mb-6">
        <div class="container">
          <h2 class="title text-center mb-4">Meet Our Team</h2>

          <div class="row">
            <div class="col-sm-6 col-lg-3">
              <div class="member member-2 text-center">
                <figure class="member-media">
                  <img
                    v-lazy="'./images/team/about-2/member-1.jpg'"
                    alt="member photo"
                  />

                  <figcaption class="member-overlay">
                    <div class="social-icons social-icons-simple">
                      <a
                        href="#"
                        class="social-icon"
                        title="Facebook"
                        target="_blank"
                      >
                        <i class="icon-facebook-f"></i>
                      </a>
                      <a
                        href="#"
                        class="social-icon"
                        title="Twitter"
                        target="_blank"
                      >
                        <i class="icon-twitter"></i>
                      </a>
                      <a
                        href="#"
                        class="social-icon"
                        title="Instagram"
                        target="_blank"
                      >
                        <i class="icon-instagram"></i>
                      </a>
                    </div>
                  </figcaption>
                </figure>

                <div class="member-content">
                  <h3 class="member-title">
                    Samanta Grey
                    <span>Founder & CEO</span>
                  </h3>
                </div>
              </div>
            </div>

            <div class="col-sm-6 col-lg-3">
              <div class="member member-2 text-center">
                <figure class="member-media">
                  <img
                    v-lazy="'./images/team/about-2/member-2.jpg'"
                    alt="member photo"
                  />

                  <figcaption class="member-overlay">
                    <div class="social-icons social-icons-simple">
                      <a
                        href="#"
                        class="social-icon"
                        title="Facebook"
                        target="_blank"
                      >
                        <i class="icon-facebook-f"></i>
                      </a>
                      <a
                        href="#"
                        class="social-icon"
                        title="Twitter"
                        target="_blank"
                      >
                        <i class="icon-twitter"></i>
                      </a>
                      <a
                        href="#"
                        class="social-icon"
                        title="Instagram"
                        target="_blank"
                      >
                        <i class="icon-instagram"></i>
                      </a>
                    </div>
                  </figcaption>
                </figure>

                <div class="member-content">
                  <h3 class="member-title">
                    Bruce Sutton
                    <span>Sales & Marketing Manager</span>
                  </h3>
                </div>
              </div>
            </div>

            <div class="col-sm-6 col-lg-3">
              <div class="member member-2 text-center">
                <figure class="member-media">
                  <img
                    v-lazy="'./images/team/about-2/member-3.jpg'"
                    alt="member photo"
                  />

                  <figcaption class="member-overlay">
                    <div class="social-icons social-icons-simple">
                      <a
                        href="#"
                        class="social-icon"
                        title="Facebook"
                        target="_blank"
                      >
                        <i class="icon-facebook-f"></i>
                      </a>
                      <a
                        href="#"
                        class="social-icon"
                        title="Twitter"
                        target="_blank"
                      >
                        <i class="icon-twitter"></i>
                      </a>
                      <a
                        href="#"
                        class="social-icon"
                        title="Instagram"
                        target="_blank"
                      >
                        <i class="icon-instagram"></i>
                      </a>
                    </div>
                  </figcaption>
                </figure>

                <div class="member-content">
                  <h3 class="member-title">
                    Janet Joy
                    <span>Product Manager</span>
                  </h3>
                </div>
              </div>
            </div>

            <div class="col-sm-6 col-lg-3">
              <div class="member member-2 text-center">
                <figure class="member-media">
                  <img
                    v-lazy="'./images/team/about-2/member-4.jpg'"
                    alt="member photo"
                  />

                  <figcaption class="member-overlay">
                    <div class="social-icons social-icons-simple">
                      <a
                        href="#"
                        class="social-icon"
                        title="Facebook"
                        target="_blank"
                      >
                        <i class="icon-facebook-f"></i>
                      </a>
                      <a
                        href="#"
                        class="social-icon"
                        title="Twitter"
                        target="_blank"
                      >
                        <i class="icon-twitter"></i>
                      </a>
                      <a
                        href="#"
                        class="social-icon"
                        title="Instagram"
                        target="_blank"
                      >
                        <i class="icon-instagram"></i>
                      </a>
                    </div>
                  </figcaption>
                </figure>

                <div class="member-content">
                  <h3 class="member-title">
                    Mark Pocket
                    <span>Product Manager</span>
                  </h3>
                </div>
              </div>
            </div>

            <div class="col-sm-6 col-lg-3">
              <div class="member member-2 text-center">
                <figure class="member-media">
                  <img
                    v-lazy="'./images/team/about-2/member-5.jpg'"
                    alt="member photo"
                  />

                  <figcaption class="member-overlay">
                    <div class="social-icons social-icons-simple">
                      <a
                        href="#"
                        class="social-icon"
                        title="Facebook"
                        target="_blank"
                      >
                        <i class="icon-facebook-f"></i>
                      </a>
                      <a
                        href="#"
                        class="social-icon"
                        title="Twitter"
                        target="_blank"
                      >
                        <i class="icon-twitter"></i>
                      </a>
                      <a
                        href="#"
                        class="social-icon"
                        title="Instagram"
                        target="_blank"
                      >
                        <i class="icon-instagram"></i>
                      </a>
                    </div>
                  </figcaption>
                </figure>

                <div class="member-content">
                  <h3 class="member-title">
                    Damion Blue
                    <span>Sales & Marketing Manager</span>
                  </h3>
                </div>
              </div>
            </div>

            <div class="col-sm-6 col-lg-3">
              <div class="member member-2 text-center">
                <figure class="member-media">
                  <img
                    v-lazy="'./images/team/about-2/member-6.jpg'"
                    alt="member photo"
                  />

                  <figcaption class="member-overlay">
                    <div class="social-icons social-icons-simple">
                      <a
                        href="#"
                        class="social-icon"
                        title="Facebook"
                        target="_blank"
                      >
                        <i class="icon-facebook-f"></i>
                      </a>
                      <a
                        href="#"
                        class="social-icon"
                        title="Twitter"
                        target="_blank"
                      >
                        <i class="icon-twitter"></i>
                      </a>
                      <a
                        href="#"
                        class="social-icon"
                        title="Instagram"
                        target="_blank"
                      >
                        <i class="icon-instagram"></i>
                      </a>
                    </div>
                  </figcaption>
                </figure>

                <div class="member-content">
                  <h3 class="member-title">
                    Lenard Smith
                    <span>Product Manager</span>
                  </h3>
                </div>
              </div>
            </div>

            <div class="col-sm-6 col-lg-3">
              <div class="member member-2 text-center">
                <figure class="member-media">
                  <img
                    v-lazy="'./images/team/about-2/member-7.jpg'"
                    alt="member photo"
                  />

                  <figcaption class="member-overlay">
                    <div class="social-icons social-icons-simple">
                      <a
                        href="#"
                        class="social-icon"
                        title="Facebook"
                        target="_blank"
                      >
                        <i class="icon-facebook-f"></i>
                      </a>
                      <a
                        href="#"
                        class="social-icon"
                        title="Twitter"
                        target="_blank"
                      >
                        <i class="icon-twitter"></i>
                      </a>
                      <a
                        href="#"
                        class="social-icon"
                        title="Instagram"
                        target="_blank"
                      >
                        <i class="icon-instagram"></i>
                      </a>
                    </div>
                  </figcaption>
                </figure>

                <div class="member-content">
                  <h3 class="member-title">
                    Rachel Green
                    <span>Product Manager</span>
                  </h3>
                </div>
              </div>
            </div>

            <div class="col-sm-6 col-lg-3">
              <div class="member member-2 text-center">
                <figure class="member-media">
                  <img
                    v-lazy="'./images/team/about-2/member-8.jpg'"
                    alt="member photo"
                  />

                  <figcaption class="member-overlay">
                    <div class="social-icons social-icons-simple">
                      <a
                        href="#"
                        class="social-icon"
                        title="Facebook"
                        target="_blank"
                      >
                        <i class="icon-facebook-f"></i>
                      </a>
                      <a
                        href="#"
                        class="social-icon"
                        title="Twitter"
                        target="_blank"
                      >
                        <i class="icon-twitter"></i>
                      </a>
                      <a
                        href="#"
                        class="social-icon"
                        title="Instagram"
                        target="_blank"
                      >
                        <i class="icon-instagram"></i>
                      </a>
                    </div>
                  </figcaption>
                </figure>

                <div class="member-content">
                  <h3 class="member-title">
                    David Doe
                    <span>Product Manager</span>
                  </h3>
                </div>
              </div>
            </div>
          </div>

          <div class="text-center mt-3">
            <nuxt-link
              :to="'/blog/listing'"
              class="btn btn-sm btn-minwidth-lg btn-outline-primary-2"
            >
              <span>LETS START WORK</span>
              <i class="icon-long-arrow-right"></i>
            </nuxt-link>
          </div>
        </div>
      </div>

      <div class="container">
        <div class="row">
          <div class="col-lg-10 offset-lg-1">
            <div class="brands-text text-center mx-auto mb-6">
              <h2 class="title">
                The world's premium design brands in one destination.
              </h2>

              <p>
                Phasellus hendrerit. Pellentesque aliquet nibh nec urna. In nisi
                neque, aliquet vel, dapibus id, mattis vel, nis
              </p>
            </div>

            <div class="brands-display">
              <div class="row justify-content-center">
                <div class="col-6 col-sm-4 col-md-3">
                  <a href="#" class="brand">
                    <img
                      v-lazy="'./images/brands/1.png'"
                      class="bg-white"
                      alt="Brand"
                    />
                  </a>
                </div>

                <div class="col-6 col-sm-4 col-md-3">
                  <a href="#" class="brand">
                    <img
                      v-lazy="'./images/brands/2.png'"
                      class="bg-white"
                      alt="Brand"
                    />
                  </a>
                </div>

                <div class="col-6 col-sm-4 col-md-3">
                  <a href="#" class="brand">
                    <img
                      v-lazy="'./images/brands/3.png'"
                      class="bg-white"
                      alt="Brand"
                    />
                  </a>
                </div>

                <div class="col-6 col-sm-4 col-md-3">
                  <a href="#" class="brand">
                    <img
                      v-lazy="'./images/brands/7.png'"
                      class="bg-white"
                      alt="Brand"
                    />
                  </a>
                </div>

                <div class="col-6 col-sm-4 col-md-3">
                  <a href="#" class="brand">
                    <img
                      v-lazy="'./images/brands/4.png'"
                      class="bg-white"
                      alt="Brand"
                    />
                  </a>
                </div>

                <div class="col-6 col-sm-4 col-md-3">
                  <a href="#" class="brand">
                    <img
                      v-lazy="'./images/brands/5.png'"
                      class="bg-white"
                      alt="Brand"
                    />
                  </a>
                </div>

                <div class="col-6 col-sm-4 col-md-3">
                  <a href="#" class="brand">
                    <img
                      v-lazy="'./images/brands/6.png'"
                      class="bg-white"
                      alt="Brand"
                    />
                  </a>
                </div>

                <div class="col-6 col-sm-4 col-md-3">
                  <a href="#" class="brand">
                    <img
                      v-lazy="'./images/brands/9.png'"
                      class="bg-white"
                      alt="Brand"
                    />
                  </a>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </main>
</template>
<script>
import CountTo from "~/components/elements/CountTo";
export default {
  components: {
    CountTo,
  },
};
</script>
