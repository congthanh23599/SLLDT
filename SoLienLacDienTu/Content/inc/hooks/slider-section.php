<?php
if (!function_exists('education_x_banner_slider_args')) :
    /**
     * Banner Slider Details
     *
     * @since Education X 1.0.0
     *
     * @return array $qargs Slider details.
     */
    function education_x_banner_slider_args()
    {
        $education_x_banner_slider_number = absint(education_x_get_option('number_of_home_slider'));
        $education_x_banner_slider_from = esc_attr(education_x_get_option('select_slider_from'));
        switch ($education_x_banner_slider_from) {
            case 'from-page':
                $education_x_banner_slider_page_list_array = array();
                for ($i = 1; $i <= $education_x_banner_slider_number; $i++) {
                    $education_x_banner_slider_page_list = education_x_get_option('select_page_for_slider_' . $i);
                    if (!empty($education_x_banner_slider_page_list)) {
                        $education_x_banner_slider_page_list_array[] = absint($education_x_banner_slider_page_list);
                    }
                }
                // Bail if no valid pages are selected.
                if (empty($education_x_banner_slider_page_list_array)) {
                    return;
                }
                /*page query*/
                $qargs = array(
                    'posts_per_page' => esc_attr($education_x_banner_slider_number),
                    'orderby' => 'post__in',
                    'post_type' => 'page',
                    'post__in' => $education_x_banner_slider_page_list_array,
                );
                return $qargs;
                break;

            case 'from-category':
                $education_x_banner_slider_category = esc_attr(education_x_get_option('select_category_for_slider'));
                $qargs = array(
                    'posts_per_page' => esc_attr($education_x_banner_slider_number),
                    'post_type' => 'post',
                    'cat' => $education_x_banner_slider_category,
                );
                return $qargs;
                break;

            default:
                break;
        }
        ?>
        <?php
    }
endif;


if (!function_exists('education_x_banner_slider')) :
    /**
     * Main Slider
     *
     * @since Education X 1.0.0
     *
     */
    function education_x_banner_slider()
    {
        $education_x_slider_button_text = esc_html(education_x_get_option('button_text_on_slider'));
        $education_x_additional_button_text_on_slider = esc_html(education_x_get_option('additional_button_text_on_slider'));
        $education_x_additional_button_url_on_slider = esc_url(education_x_get_option('additional_button_url_on_slider'));
        $education_x_slider_excerpt_number = absint(education_x_get_option('number_of_content_home_slider'));
        if (1 != education_x_get_option('show_slider_section')) {
            return null;
        }
        $education_x_banner_slider_args = education_x_banner_slider_args();
        $education_x_banner_slider_query = new WP_Query($education_x_banner_slider_args); ?>
        <!-- slider Section -->
        <section class="main-banner">
            <?php $rtl_class = 'false';
            if(is_rtl()){ 
                $rtl_class = 'true';
            }?>
            <div class="mainbanner-jumbotron" data-slick='{"rtl": <?php echo($rtl_class); ?>}'>
                <?php
                if ($education_x_banner_slider_query->have_posts()) :
                    while ($education_x_banner_slider_query->have_posts()) : $education_x_banner_slider_query->the_post();
                        if (has_post_thumbnail()) {
                            $thumb = wp_get_attachment_image_src(get_post_thumbnail_id(get_the_ID()), 'full');
                            $url = $thumb['0'];
                        } else {
                            $url = NULL;
                        }
                        if (has_excerpt()) {
                            $education_x_slider_content = get_the_excerpt();
                        } else {
                            $education_x_slider_content = education_x_words_count($education_x_slider_excerpt_number, get_the_content());
                        }
                        ?>
                        <figure class="slick-item primary-bgcolor">
                            <a href="<?php the_permalink(); ?>" class="bg-image data-bg-slide">
                                <img src="<?php echo esc_url($url); ?>">
                            </a>
                            <figcaption class="slider-figcaption">
                                <div class="table-align">
                                    <div class="table-align-cell v-align-bottom">
                                        <div class="slider-wrapper">
                                            <h2 class="slide-title">
                                                <?php the_title(); ?>
                                            </h2>
                                            <div class="slider-content hidden-xs visible">
                                                <?php echo wp_kses_post($education_x_slider_content); ?>
                                            </div>
                                            <?php if (!empty ($education_x_slider_button_text)) { ?>
                                                <a href="<?php the_permalink(); ?>" class="btn btn-lg btn-primary tm-shadow">
                                                    <span><?php echo esc_html($education_x_slider_button_text); ?></span>
                                                    <i class="fa fa-angle-right"></i>
                                                </a>
                                            <?php } ?>
                                            <?php if (!empty ($education_x_additional_button_text_on_slider)) { ?>
                                                <a href="<?php echo esc_url($education_x_additional_button_url_on_slider); ?>" class="btn btn-lg btn-border tm-shadow">
                                                    <span><?php echo esc_html($education_x_additional_button_text_on_slider); ?></span>
                                                    <i class="fa fa-angle-right"></i>
                                                </a>
                                            <?php } ?>
                                        </div>
                                    </div>
                                </div>
                            </figcaption>
                            <?php if (1 == education_x_get_option('banner_overlay_slider')) { ?>
                                <div class="bg-overlay bg-overlay-enable"></div>
                            <?php } ?>
                        </figure>
                        <?php
                        endwhile;
                endif;
                wp_reset_postdata();
                ?>
            </div>
            <?php if (1 == education_x_get_option('show_about_section')) { ?>
                <div class="scroll-down-content">
                    <div class="sd-img-icon">
                        <a href="#about-us" class="smoothscroll">
                            <div class="mouse-icon"><div class="wheel"></div></div>
                        </a>
                    </div>
                </div>
            <?php } ?>
        </section>
        <!-- end slider-section -->
        <?php
    }
endif;
add_action('education_x_action_slider_section', 'education_x_banner_slider', 10);
