<?php
if (!function_exists('education_x_callback_section')) :
    /**
     * Tab callback Details
     *
     * @since Education X 1.0.0
     *
     */
    function education_x_callback_section()
    {
        $education_x_callback_button_enable = education_x_get_option('show_page_link_button');
        $education_x_callback_button_text = education_x_get_option('callback_button_text');
        $education_x_callback_button_url = education_x_get_option('callback_button_link');
        $education_x_callback_excerpt_number = absint(education_x_get_option('number_of_content_home_callback'));
        $education_x_callback_page = array();
        $education_x_callback_page[] = esc_attr(education_x_get_option('select_callback_page'));
        if (1 != education_x_get_option('show_our_callback_section')) {
            return null;
        }
        if (!empty($education_x_callback_page)) {
            $education_x_callback_page_args = array(
                'post_type' => 'page',
                'post__in' => $education_x_callback_page,
                'orderby' => 'post__in'
            );
        }
        if (!empty($education_x_callback_page_args)) {
            $education_x_callback_page_query = new WP_Query($education_x_callback_page_args);
            while ($education_x_callback_page_query->have_posts()): $education_x_callback_page_query->the_post();
                if (has_excerpt()) {
                    $education_x_callback_main_content = get_the_excerpt();
                } else {
                    $education_x_callback_main_content = education_x_words_count($education_x_callback_excerpt_number, get_the_content());
                }
                if (has_post_thumbnail()) {
                    $thumb = wp_get_attachment_image_src(get_post_thumbnail_id(get_the_ID()), 'medium_large');
                    $url = $thumb['0'];
                } else {
                    $url = NULL;
                }
                ?>
                <!--CTA Starts-->
                <section class="section-block section-block-1 grid-cta-section">
                    <div class="container">
                        <div class="row align-items-center no-gutters grid-cta-bg">
                            <div class="col-md-6">
                                <div class="grid-cta-img">
                                    <img src="<?php echo esc_url($url); ?>">
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="section-content">
                                    <div class="section-header">
                                        <h2 class="section-title section-title1">
                                            <span>
                                               <?php the_title(); ?>
                                            </span>
                                        </h2>
                                    </div>
                                    <div class="cta-description small-font clearfix">
                                        <?php echo esc_html($education_x_callback_main_content); ?>
                                    </div>
                                    <div class="cta-btn-group" role="group" aria-label="buttons">
                                        <?php if ($education_x_callback_button_enable == 1) { ?>
                                            <a href="<?php the_permalink(); ?>"
                                               class="btn btn-sm btn-primary tm-shadow"><?php _e('Learn More', 'education-x'); ?></a>
                                        <?php } ?>
                                        <?php if (!empty($education_x_callback_button_text)) { ?>
                                            <a href="<?php echo esc_url($education_x_callback_button_url); ?>"
                                               class="btn btn-sm btn-border"><?php echo esc_html($education_x_callback_button_text); ?></a>
                                        <?php } ?>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
                <!--CTA Ends-->
            <?php endwhile;
            wp_reset_postdata();
        } ?>
        <?php
    }
endif;
add_action('education_x_action_front_page', 'education_x_callback_section', 40);